using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.EXT;
using Silk.NET.Vulkan.Extensions.KHR;

namespace SilkNetConvenience; 

public static unsafe class InstanceExtensions {
	public static PhysicalDevice[] EnumeratePhysicalDevices(this Vk self, Instance instance) {
		return Helpers.GetArray((ref uint len, PhysicalDevice* data) => self.EnumeratePhysicalDevices(instance, ref len, data));
	}
	
	public static ExtDebugUtils? GetDebugUtilsExtension(this Vk vk, Instance instance) {
		return vk.TryGetInstanceExtension(instance, out ExtDebugUtils extension) ? extension : null;
	}

	public static KhrSurface? GetKhrSurfaceExtension(this Vk vk, Instance instance) {
		return vk.TryGetInstanceExtension(instance, out KhrSurface extension) ? extension : null;
	}
	
	public static KhrSwapchain? GetKhrSwapchainExtension(this Vk vk, Instance instance) {
		return vk.TryGetInstanceExtension(instance, out KhrSwapchain extension) ? extension : null;
	}
}