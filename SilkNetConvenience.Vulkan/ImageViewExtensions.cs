using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo.Images;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class ImageViewExtensions {
	public static ImageView CreateImageView(this Vk vk, Device device, ImageViewCreateInformation createInfo) {
		var info = createInfo.GetCreateInfo();
		vk.CreateImageView(device, info, null, out var imageView).AssertSuccess();
		return imageView;
	}
	
	public static void DestroyImageView(this Vk vk, Device device, ImageView imageView) {
		vk.DestroyImageView(device, imageView, null);
	}
}