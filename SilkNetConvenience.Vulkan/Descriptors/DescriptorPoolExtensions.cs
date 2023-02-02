using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Descriptors; 

public static unsafe class DescriptorPoolExtensions {
	public static DescriptorPool CreateDescriptorPool(this Vk vk, Device device, DescriptorPoolCreateInformation createInfo) {
		using var info = createInfo.GetCreateInfo();
		vk.CreateDescriptorPool(device, info.Resource, null, out var descriptorPool).AssertSuccess();
		return descriptorPool;
	}

	public static void DestroyDescriptorPool(this Vk vk, Device device, DescriptorPool descriptorPool) {
		vk.DestroyDescriptorPool(device, descriptorPool, null);
	}
}