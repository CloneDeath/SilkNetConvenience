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
}