using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Descriptors; 

public static unsafe class DescriptorSetLayoutExtensions {
	public static DescriptorSetLayout CreateDescriptorSetLayout(this Vk vk, Device device, 
																DescriptorSetLayoutCreateInformation createInfo) {
		using var info = createInfo.GetCreateInfo();
		vk.CreateDescriptorSetLayout(device, info.Resource, null, out var layout).AssertSuccess();
		return layout;
	}

	public static void DestroyDescriptorSetLayout(this Vk vk, Device device, DescriptorSetLayout layout) {
		vk.DestroyDescriptorSetLayout(device, layout, null);
	}
}