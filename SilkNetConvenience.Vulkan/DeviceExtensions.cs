using System.Linq;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class DeviceExtensions {
	public static Queue GetDeviceQueue(this Vk vk, Device device, uint queueFamilyIndex, uint queueIndex) {
		vk.GetDeviceQueue(device, queueFamilyIndex, queueIndex, out var queue);
		return queue;
	}

	public static DeviceMemory AllocateMemory(this Vk vk, Device device, MemoryAllocateInformation allocInfo) {
		var memoryAllocateInfo = new MemoryAllocateInfo {
			SType = StructureType.MemoryAllocateInfo,
			AllocationSize = allocInfo.AllocationSize,
			MemoryTypeIndex = allocInfo.MemoryTypeIndex
		};
		vk.AllocateMemory(device, memoryAllocateInfo, null, out var deviceMemory).AssertSuccess();
		return deviceMemory;
	}

	public static Silk.NET.Vulkan.Buffer CreateBuffer(this Vk vk, Device device, BufferCreateInformation bufferCreateInfo) {
		fixed (uint* queueFamilyIndicesPointer = bufferCreateInfo.QueueFamilyIndices) {
			var createInfo = new BufferCreateInfo {
				SType = StructureType.BufferCreateInfo,
				Flags = bufferCreateInfo.Flags,
				Size = bufferCreateInfo.Size,
				Usage = bufferCreateInfo.Usage,
				SharingMode = bufferCreateInfo.SharingMode,
				PQueueFamilyIndices = queueFamilyIndicesPointer,
				QueueFamilyIndexCount = (uint)bufferCreateInfo.QueueFamilyIndices.Length
			};
			vk.CreateBuffer(device, createInfo, null, out var buffer).AssertSuccess();
			return buffer;
		}
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

	public static CommandBuffer[] AllocateCommandBuffers(this Vk vk, Device device, CommandBufferAllocateInformation allocInfo) {
		var infos = new[]{new CommandBufferAllocateInfo {
			SType = StructureType.CommandBufferAllocateInfo,
			CommandPool = allocInfo.CommandPool,
			Level = allocInfo.Level,
			CommandBufferCount = allocInfo.CommandBufferCount
		}};
		var commandBuffers = new CommandBuffer[allocInfo.CommandBufferCount];
		vk.AllocateCommandBuffers(device, infos, commandBuffers).AssertSuccess();
		return commandBuffers;
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
		var writes = writeInfos.Select(w => {
			fixed (DescriptorImageInfo* imageInfo = w.ImageInfo)
			fixed (DescriptorBufferInfo* bufferInfo = w.BufferInfo)
			fixed (BufferView* bufferView = w.TexelBufferView) {
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
}