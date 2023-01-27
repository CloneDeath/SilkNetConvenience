using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;

namespace SilkNetConvenience.Wrappers; 

public class VulkanFramebuffer : BaseVulkanWrapper {
	public Vk Vk;
	public Device Device;
	public Framebuffer Framebuffer;

	public VulkanFramebuffer(VulkanDevice device, FramebufferCreateInformation createInfo) : this(device.Vk, device.Device, createInfo){}
	public VulkanFramebuffer(Vk vk, Device device, FramebufferCreateInformation createInfo) {
		Vk = vk;
		Device = device;
		Framebuffer = vk.CreateFramebuffer(device, createInfo);
	}
	protected override void ReleaseVulkanResources() {
		Vk.DestroyFramebuffer(Device, Framebuffer);
	}
}