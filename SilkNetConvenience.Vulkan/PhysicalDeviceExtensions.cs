using System.Linq;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class PhysicalDeviceExtensions {
	public static PhysicalDeviceProperties GetPhysicalDeviceProperties(this Vk vk, PhysicalDevice physicalDevice) {
		vk.GetPhysicalDeviceProperties(physicalDevice, out var properties);
		return properties;
	}
	
	public static PhysicalDeviceFeatures GetPhysicalDeviceFeatures(this Vk vk, PhysicalDevice physicalDevice) {
		vk.GetPhysicalDeviceFeatures(physicalDevice, out var features);
		return features;
	}

	public static FormatProperties GetPhysicalDeviceFormatProperties(this Vk vk, PhysicalDevice physicalDevice, Format format) {
		vk.GetPhysicalDeviceFormatProperties(physicalDevice, format, out var properties);
		return properties;
	}
	
	public static QueueFamilyProperties[] GetPhysicalDeviceQueueFamilyProperties(this Vk vk, PhysicalDevice physicalDevice) {
		return Helpers.GetArray((ref uint len, QueueFamilyProperties* data) => vk.GetPhysicalDeviceQueueFamilyProperties(physicalDevice, ref len, data));
	}
	
	public static ExtensionProperties[] EnumerateDeviceExtensionProperties(this Vk vk, PhysicalDevice physicalDevice, string? layerName = null) {
		return Helpers.GetArray((ref uint len, ExtensionProperties* data)
			=> vk.EnumerateDeviceExtensionProperties(physicalDevice, layerName, ref len, data));
	}

	public static PhysicalDeviceMemoryProperties GetPhysicalDeviceMemoryProperties(this Vk vk, PhysicalDevice physicalDevice) {
		vk.GetPhysicalDeviceMemoryProperties(physicalDevice, out var memoryProperties);
		return memoryProperties;
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
}