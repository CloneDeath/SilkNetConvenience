using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;

namespace SilkNetConvenience.Wrappers; 

public class VulkanRenderPass : BaseVulkanWrapper {
	public Vk Vk;
	public Device Device;
	public RenderPass RenderPass;

	public VulkanRenderPass(VulkanDevice device, RenderPassCreateInformation createInfo) 
		: this(device.Vk, device.Device, createInfo) {
		device.AddChildResource(this);
	}
	public VulkanRenderPass(Vk vk, Device device, RenderPassCreateInformation createInfo) {
		Vk = vk;
		Device = device;
		RenderPass = vk.CreateRenderPass(device, createInfo);
	}
	
	protected override void ReleaseVulkanResources() {
		Vk.DestroyRenderPass(Device, RenderPass);
	}
	
	public static implicit operator RenderPass(VulkanRenderPass self) => self.RenderPass;
}