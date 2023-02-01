using Silk.NET.Vulkan;

namespace SilkNetConvenience; 

public static unsafe class VulkanExtensions {
	public static LayerProperties[] EnumerateInstanceLayerProperties(this Vk self) {
		return Helpers.GetArray((ref uint length, LayerProperties* data) => self.EnumerateInstanceLayerProperties(ref length, data));
	}

	public static void DestroyDevice(this Vk vk, Device device) {
		vk.DestroyDevice(device, null);
	}
}