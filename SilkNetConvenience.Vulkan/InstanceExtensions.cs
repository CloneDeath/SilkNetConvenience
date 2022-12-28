using Silk.NET.Vulkan;

namespace SilkNetConvenience; 

public static unsafe class InstanceExtensions {
	public static PhysicalDevice[] EnumeratePhysicalDevices(this Vk self, Instance instance) {
		return Helpers.GetArray((ref uint len, PhysicalDevice* data) => self.EnumeratePhysicalDevices(instance, ref len, data));
	}
}