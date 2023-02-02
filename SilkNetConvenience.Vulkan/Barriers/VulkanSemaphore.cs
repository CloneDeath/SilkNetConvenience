using Silk.NET.Vulkan;
using SilkNetConvenience.Devices;

namespace SilkNetConvenience.Barriers; 

public class VulkanSemaphore : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly Semaphore Semaphore;

	public VulkanSemaphore(VulkanDevice device) : this(device.Vk, device.Device) {
		device.AddChildResource(this);
	}
	public VulkanSemaphore(Vk vk, Device device) {
		Vk = vk;
		Device = device;
		Semaphore = Vk.CreateSemaphore(Device);
	}
	
	protected override void ReleaseVulkanResources() {
		Vk.DestroySemaphore(Device, Semaphore);
	}
	
	public static implicit operator Semaphore(VulkanSemaphore self) => self.Semaphore;
}