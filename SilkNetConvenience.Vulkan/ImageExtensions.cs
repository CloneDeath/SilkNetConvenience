using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo.Images;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class ImageExtensions {
	public static Image CreateImage(this Vk vk, Device device, ImageCreateInformation createInfo) {
		var info = createInfo.GetCreateInfo();
		vk.CreateImage(device, info, null, out var image).AssertSuccess();
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