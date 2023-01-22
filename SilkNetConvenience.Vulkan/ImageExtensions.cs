using Silk.NET.Vulkan;

namespace SilkNetConvenience; 

public static class ImageExtensions {
	public static MemoryRequirements GetImageMemoryRequirements(this Vk vk, Device device, Image image) {
		vk.GetImageMemoryRequirements(device, image, out var memoryRequirements);
		return memoryRequirements;
	}
}