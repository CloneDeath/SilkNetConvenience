using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Devices; 

public static unsafe class DeviceExtensions {
	public static Device CreateDevice(this Vk vk, PhysicalDevice physicalDevice, DeviceCreateInformation createInfo) {
		using var info = createInfo.GetCreateInfo();
		vk.CreateDevice(physicalDevice, info.Resource, null, out var device).AssertSuccess();
		return device;
	}

	public static void DestroyDevice(this Vk vk, Device device) {
		vk.DestroyDevice(device, null);
	}
	
	public static KhrSwapchain? GetKhrSwapchainExtension(this Vk vk, Instance instance, Device device) {
		return vk.TryGetDeviceExtension(instance, device, out KhrSwapchain extension) ? extension : null;
	}
}