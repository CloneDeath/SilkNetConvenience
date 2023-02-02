using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.EXT;
using Silk.NET.Vulkan.Extensions.KHR;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Instances; 

public static unsafe class InstanceExtensions {
	public static Instance CreateInstance(this Vk vk, InstanceCreateInformation createInfo) {
		using var info = createInfo.GetCreateInfo();
		vk.CreateInstance(info.Resource, null, out var instance).AssertSuccess();
		return instance;
	}
	
	public static void DestroyInstance(this Vk vk, Instance instance) {
		vk.DestroyInstance(instance, null);
	}
	
	public static ExtDebugUtils? GetDebugUtilsExtension(this Vk vk, Instance instance) {
		return vk.TryGetInstanceExtension(instance, out ExtDebugUtils extension) ? extension : null;
	}

	public static KhrSurface? GetKhrSurfaceExtension(this Vk vk, Instance instance) {
		return vk.TryGetInstanceExtension(instance, out KhrSurface extension) ? extension : null;
	}
}