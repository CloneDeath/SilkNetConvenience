using Silk.NET.Vulkan;

namespace SilkNetConvenience.Wrappers; 

public class VulkanSemaphore : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly Semaphore Semaphore;
	
	public VulkanSemaphore(VulkanDevice device) : this(device.Vk, device.Device){}
	public VulkanSemaphore(Vk vk, Device device) {
		Vk = vk;
		Device = device;
		Semaphore = Vk.CreateSemaphore(Device);
	}
	
	protected override void ReleaseVulkanResources() {
		Vk.DestroySemaphore(Device, Semaphore);
	}
}