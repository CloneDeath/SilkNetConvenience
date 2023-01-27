using System.Linq;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.CreateInfo.Descriptors;
using SilkNetConvenience.CreateInfo.Images;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class DeviceExtensions {
	public static KhrSwapchain? GetKhrSwapchainExtension(this Vk vk, Instance instance, Device device) {
		return vk.TryGetDeviceExtension(instance, device, out KhrSwapchain extension) ? extension : null;
	}

	public static ShaderModule CreateShaderModule(this Vk vk, Device device, ShaderModuleCreateInformation shaderModuleCreateInfo) {
		fixed (byte* codePointer = shaderModuleCreateInfo.Code) {
			var createInfo = new ShaderModuleCreateInfo {
				SType = StructureType.ShaderModuleCreateInfo,
				CodeSize = (uint)shaderModuleCreateInfo.Code.Length,
				PCode = (uint*)codePointer
			};
			vk.CreateShaderModule(device, createInfo, null, out var shaderModule).AssertSuccess();
			return shaderModule;
		}
	}

	public static DescriptorSetLayout CreateDescriptorSetLayout(this Vk vk, Device device, DescriptorSetLayoutCreateInformation descriptorSetLayoutCreateInfo) {
		var bindings = descriptorSetLayoutCreateInfo.Bindings
			.Select(b => {
				fixed (Sampler* samplerPointer = b.ImmutableSamplers) {
					return new DescriptorSetLayoutBinding {
						Binding = b.Binding,
						DescriptorType = b.DescriptorType,
						DescriptorCount = b.DescriptorCount,
						StageFlags = b.StageFlags,
						PImmutableSamplers = samplerPointer
					};
				}
			}).ToArray();

		fixed (DescriptorSetLayoutBinding* bindingsPointer = bindings) {
			var createInfo = new DescriptorSetLayoutCreateInfo {
				SType = StructureType.DescriptorSetLayoutCreateInfo,
				Flags = descriptorSetLayoutCreateInfo.Flags,
				PBindings = bindingsPointer,
				BindingCount = (uint)bindings.Length
			};
			vk.CreateDescriptorSetLayout(device, createInfo, null, out var layout).AssertSuccess();
			return layout;
		}
	}

	public static PipelineLayout CreatePipelineLayout(this Vk vk, Device device, PipelineLayoutCreateInformation pipelineLayoutCreateInfo) {
		fixed (PushConstantRange* pushConstantsPointer = pipelineLayoutCreateInfo.PushConstantRanges)
		fixed (DescriptorSetLayout* setLayoutsPointer = pipelineLayoutCreateInfo.SetLayouts) {
			var createInfo = new PipelineLayoutCreateInfo {
				SType = StructureType.PipelineLayoutCreateInfo,
				Flags = pipelineLayoutCreateInfo.Flags,
				SetLayoutCount = (uint)pipelineLayoutCreateInfo.SetLayouts.Length,
				PSetLayouts = setLayoutsPointer,
				PushConstantRangeCount = (uint)pipelineLayoutCreateInfo.PushConstantRanges.Length,
				PPushConstantRanges = pushConstantsPointer
			};
			vk.CreatePipelineLayout(device, createInfo, null, out var pipelineLayout).AssertSuccess();
			return pipelineLayout;
		}
	}

	public static Pipeline CreateComputePipeline(this Vk vk, Device device, PipelineCache pipelineCache,
		ComputePipelineCreateInformation pipelineCreateInformation) {
		return vk.CreateComputePipelines(device, pipelineCache, new[] { pipelineCreateInformation }).First();
	}

	public static Pipeline[] CreateComputePipelines(this Vk vk, Device device, PipelineCache pipelineCache,
													ComputePipelineCreateInformation[] pipelineCreateInfos) {
		var createInfos = pipelineCreateInfos.Select(p => new ComputePipelineCreateInfo {
			SType = StructureType.ComputePipelineCreateInfo,
			Flags = p.Flags,
			Layout = p.Layout,
			Stage = new PipelineShaderStageCreateInfo {
				SType = StructureType.PipelineShaderStageCreateInfo,
				Stage = p.Stage.Stage,
				Flags = p.Stage.Flags,
				Module = p.Stage.Module,
				PName = (byte*)SilkMarshal.StringToPtr(p.Stage.Name)
			},
			BasePipelineHandle = p.BasePipelineHandle,
			BasePipelineIndex = p.BasePipelineIndex
		}).ToArray();

		var pipelines = new Pipeline[pipelineCreateInfos.Length];

		try {
			vk.CreateComputePipelines(device, pipelineCache, createInfos, null, pipelines).AssertSuccess();
			return pipelines;
		}
		finally {
			foreach (var createInfo in createInfos) {
				SilkMarshal.Free((nint)createInfo.Stage.PName);
			}
		}
	}

	public static CommandPool CreateCommandPool(this Vk vk, Device device, CommandPoolCreateInformation commandPoolCreateInfo) {
		var createInfo = new CommandPoolCreateInfo {
			SType = StructureType.CommandPoolCreateInfo,
			QueueFamilyIndex = commandPoolCreateInfo.QueueFamilyIndex,
			Flags = commandPoolCreateInfo.Flags
		};
		vk.CreateCommandPool(device, createInfo, null, out var commandPool).AssertSuccess();
		return commandPool;
	}

	public static DescriptorPool CreateDescriptorPool(this Vk vk, Device device, DescriptorPoolCreateInformation descriptorPoolCreateInfo) {
		fixed (DescriptorPoolSize* poolSizesPointer = descriptorPoolCreateInfo.PoolSizes) {
			var createInfo = new DescriptorPoolCreateInfo {
				SType = StructureType.DescriptorPoolCreateInfo,
				Flags = descriptorPoolCreateInfo.Flags,
				MaxSets = descriptorPoolCreateInfo.MaxSets,
				PoolSizeCount = (uint)descriptorPoolCreateInfo.PoolSizes.Length,
				PPoolSizes = poolSizesPointer
			};
			vk.CreateDescriptorPool(device, createInfo, null, out var descriptorPool).AssertSuccess();
			return descriptorPool;
		}
	}

	public static DescriptorSet[] AllocateDescriptorSets(this Vk vk, Device device, DescriptorSetAllocateInformation descriptorSetAllocateInfo) {
		fixed (DescriptorSetLayout* setLayoutsPointer = descriptorSetAllocateInfo.SetLayouts) {
			var allocInfo = new[]{new DescriptorSetAllocateInfo {
				SType = StructureType.DescriptorSetAllocateInfo,
				DescriptorPool = descriptorSetAllocateInfo.DescriptorPool,
				DescriptorSetCount = (uint)descriptorSetAllocateInfo.SetLayouts.Length,
				PSetLayouts = setLayoutsPointer
			}};
			var results = new DescriptorSet[descriptorSetAllocateInfo.SetLayouts.Length];
			vk.AllocateDescriptorSets(device, allocInfo, results).AssertSuccess();
			return results;
		}
	}

	public static void UpdateDescriptorSets(this Vk vk, Device device, WriteDescriptorSetInfo[] writeInfos, CopyDescriptorSetInfo[] copyInfos) {
		var imageInfos = writeInfos.Select(w => w.ImageInfo).ToArray();
		var bufferInfos = writeInfos.Select(w => w.BufferInfo).ToArray();
		var bufferViews = writeInfos.Select(w => w.TexelBufferView).ToArray();

		var writes = writeInfos.Select((w, i) => {
			fixed (DescriptorImageInfo* imageInfo = imageInfos[i])
			fixed (DescriptorBufferInfo* bufferInfo = bufferInfos[i])
			fixed (BufferView* bufferView = bufferViews[i]) {
				return new WriteDescriptorSet {
					SType = StructureType.WriteDescriptorSet,
					DstSet = w.DstSet,
					DstBinding = w.DstBinding,
					DstArrayElement = w.DstArrayElement,
					DescriptorCount = w.DescriptorCount,
					DescriptorType = w.DescriptorType,
					PImageInfo = imageInfo,
					PBufferInfo = bufferInfo,
					PTexelBufferView = bufferView
				};
			}
		}).ToArray();
		var copies = copyInfos.Select(c => new CopyDescriptorSet {
			SType = StructureType.CopyDescriptorSet,
			SrcSet = c.SrcSet,
			SrcBinding = c.SrcBinding,
			SrcArrayElement = c.SrcArrayElement,
			DstSet = c.DstSet,
			DstBinding = c.DstBinding,
			DstArrayElement = c.DstArrayElement,
			DescriptorCount = c.DescriptorCount
		}).ToArray();
		vk.UpdateDescriptorSets(device, writes, copies);
	}

	public static Sampler CreateSampler(this Vk vk, Device device, SamplerCreateInformation createInfo) {
		var info = createInfo.GetCreateInfo();
		vk.CreateSampler(device, info, null, out var sampler).AssertSuccess();
		return sampler;
	}

	public static void DestroySampler(this Vk vk, Device device, Sampler sampler) {
		vk.DestroySampler(device, sampler, null);
	}

	public static void DestroyDescriptorPool(this Vk vk, Device device, DescriptorPool descriptorPool) {
		vk.DestroyDescriptorPool(device, descriptorPool, null);
	}

	public static void DestroyDescriptorSetLayout(this Vk vk, Device device, DescriptorSetLayout layout) {
		vk.DestroyDescriptorSetLayout(device, layout, null);
	}

	public static void DestroyPipelineLayout(this Vk vk, Device device, PipelineLayout layout) {
		vk.DestroyPipelineLayout(device, layout, null);
	}

	public static void DestroyShaderModule(this Vk vk, Device device, ShaderModule shaderModule) {
		vk.DestroyShaderModule(device, shaderModule, null);
	}

	public static void DestroyPipeline(this Vk vk, Device device, Pipeline pipeline) {
		vk.DestroyPipeline(device, pipeline, null);
	}

	public static void DestroyCommandPool(this Vk vk, Device device, CommandPool commandPool) {
		vk.DestroyCommandPool(device, commandPool, null);
	}
}