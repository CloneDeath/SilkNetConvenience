using Silk.NET.Vulkan;

namespace SilkNetConvenience; 

public static unsafe class ImageViewExtensions {
	public static void DestroyImageView(this Vk vk, Device device, ImageView imageView) {
		vk.DestroyImageView(device, imageView, null);
	}
}