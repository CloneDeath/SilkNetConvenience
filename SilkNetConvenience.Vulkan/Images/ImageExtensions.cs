using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Images; 

public static unsafe class ImageExtensions {
	public static Image CreateImage(this Vk vk, Device device, ImageCreateInformation createInfo) {
		using var info = createInfo.GetCreateInfo();
		vk.CreateImage(device, info.Resource, null, out var image).AssertSuccess();
		return image;
	}
	
	public static void DestroyImage(this Vk vk, Device device, Image image) {
		vk.DestroyImage(device, image, null);
	}
	
	public static MemoryRequirements GetImageMemoryRequirements(this Vk vk, Device device, Image image) {
		vk.GetImageMemoryRequirements(device, image, out var memoryRequirements);
		return memoryRequirements;
	}
}