using Silk.NET.Vulkan;

namespace SilkNetConvenience; 

public static unsafe class VulkanExtensions {
	public static LayerProperties[] EnumerateInstanceLayerProperties(this Vk self) {
		return Helpers.GetArray((ref uint length, LayerProperties* data) => self.EnumerateInstanceLayerProperties(ref length, data));
	}
}