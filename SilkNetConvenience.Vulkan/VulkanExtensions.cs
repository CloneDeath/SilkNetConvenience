using System;
using System.Linq;
using System.Runtime.InteropServices;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class VulkanExtensions {
	public static Instance CreateInstance(this Vk self, InstanceCreateInformation info) {
		var appInfo = new ApplicationInfo {
			SType = StructureType.ApplicationInfo,
			ApiVersion = info.ApplicationInfo.ApiVersion,
			ApplicationVersion = info.ApplicationInfo.ApplicationVersion,
			EngineVersion = info.ApplicationInfo.EngineVersion,
			PApplicationName = (byte*)Marshal.StringToHGlobalUni(info.ApplicationInfo.ApplicationName),
			PEngineName = (byte*)Marshal.StringToHGlobalUni(info.ApplicationInfo.EngineName)
		};
		var instanceCreateInfo = new InstanceCreateInfo {
			SType = StructureType.InstanceCreateInfo,
			PApplicationInfo = &appInfo,
			EnabledExtensionCount = (uint)info.EnabledExtensions.Length,
			PpEnabledExtensionNames = (byte**)SilkMarshal.StringArrayToPtr(info.EnabledExtensions),
			EnabledLayerCount = (uint)info.EnabledLayerNames.Length,
			PpEnabledLayerNames = (byte**)SilkMarshal.StringArrayToPtr(info.EnabledLayerNames)
		};

		try {
			self.CreateInstance(instanceCreateInfo, null, out var instance).AssertSuccess();
			return instance;
		}
		finally {
			Marshal.FreeHGlobal((IntPtr)appInfo.PApplicationName);
			Marshal.FreeHGlobal((IntPtr)appInfo.PEngineName);
			if (info.EnabledExtensions.Any()) {
				SilkMarshal.Free((nint)instanceCreateInfo.PpEnabledExtensionNames);
			}

			if (info.EnabledLayerNames.Any()) {
				SilkMarshal.Free((nint)instanceCreateInfo.PpEnabledLayerNames);
			}
		}
	}

	public static LayerProperties[] EnumerateInstanceLayerProperties(this Vk self) {
		return Helpers.GetArray((ref uint length, LayerProperties* data) => self.EnumerateInstanceLayerProperties(ref length, data));
	}

	public static PhysicalDevice[] EnumeratePhysicalDevices(this Vk self, Instance instance) {
		return Helpers.GetArray((ref uint len, PhysicalDevice* data) => self.EnumeratePhysicalDevices(instance, ref len, data));
	}

	public static QueueFamilyProperties[] GetPhysicalDeviceQueueFamilyProperties(this Vk vk, PhysicalDevice physicalDevice) {
		return Helpers.GetArray((ref uint len, QueueFamilyProperties* data) => vk.GetPhysicalDeviceQueueFamilyProperties(physicalDevice, ref len, data));
	}

	public static Device CreateDevice(this Vk vk, PhysicalDevice physicalDevice, DeviceCreateInformation info) {
		var enabledFeatures = info.EnabledFeatures;
		var queueCreateInfos = info.QueueCreateInfos.Select(q => {
			fixed (float* queuePrioritiesPointer = q.QueuePriorities) {
				return new DeviceQueueCreateInfo {
					SType = StructureType.DeviceQueueCreateInfo,
					Flags = q.Flags,
					QueueFamilyIndex = q.QueueFamilyIndex,
					QueueCount = (uint)q.QueuePriorities.Length,
					PQueuePriorities = queuePrioritiesPointer
				};
			}
		}).ToArray();
		fixed (DeviceQueueCreateInfo* queueCreateInfoPointer = queueCreateInfos) {
			var deviceCreateInfo = new DeviceCreateInfo {
				SType = StructureType.DeviceCreateInfo,
				EnabledLayerCount = (uint)info.EnabledLayerNames.Length,
				PpEnabledLayerNames = (byte**)SilkMarshal.StringArrayToPtr(info.EnabledLayerNames),
				EnabledExtensionCount = (uint)info.EnabledExtensionNames.Length,
				PpEnabledExtensionNames = (byte**)SilkMarshal.StringArrayToPtr(info.EnabledExtensionNames),
				PEnabledFeatures = &enabledFeatures,
				QueueCreateInfoCount = (uint)queueCreateInfos.Length,
				PQueueCreateInfos = queueCreateInfoPointer
			};
			try {
				vk.CreateDevice(physicalDevice, deviceCreateInfo, null, out var device).AssertSuccess();
				return device;
			}
			finally {
				SilkMarshal.Free((nint)deviceCreateInfo.PpEnabledLayerNames);
				SilkMarshal.Free((nint)deviceCreateInfo.PpEnabledExtensionNames);
			}
		}
	}

	public static Queue GetDeviceQueue(this Vk vk, Device device, uint queueFamilyIndex, uint queueIndex) {
		vk.GetDeviceQueue(device, queueFamilyIndex, queueIndex, out var queue);
		return queue;
	}

	public static PhysicalDeviceMemoryProperties GetPhysicalDeviceMemoryProperties(this Vk vk, PhysicalDevice physicalDevice) {
		vk.GetPhysicalDeviceMemoryProperties(physicalDevice, out var memoryProperties);
		return memoryProperties;
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
}

public class CommandBufferAllocateInformation {
	public CommandPool CommandPool;
	public CommandBufferLevel Level;
	public uint CommandBufferCount;
}