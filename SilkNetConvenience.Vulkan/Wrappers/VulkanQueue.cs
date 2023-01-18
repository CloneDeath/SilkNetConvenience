using Silk.NET.Vulkan;

namespace SilkNetConvenience.Wrappers;

public class VulkanQueue {
	public readonly Queue Queue;
	
	public VulkanQueue(VulkanDevice device, uint queueFamilyIndex, uint queueIndex)
		: this(device.Vk, device.Device, queueFamilyIndex, queueIndex){ }
	public VulkanQueue(Vk vk, Device device, uint queueFamilyIndex, uint queueIndex) {
		vk.GetDeviceQueue(device, queueFamilyIndex, queueIndex, out Queue);
	}
}