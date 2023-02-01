using Silk.NET.Vulkan;

namespace SilkNetConvenience.Wrappers.KHR; 

public class VulkanSwapchainImage : VulkanImage {
	public VulkanSwapchainImage(VulkanSwapchain swapchain, Image image) : base(swapchain.Vk, swapchain.Device, image){}
	public VulkanSwapchainImage(VulkanDevice device, Image image) : base(device, image) { }
	public VulkanSwapchainImage(Vk vk, Device device, Image image) : base(vk, device, image) { }

	protected override void ReleaseVulkanResources() { }
	
	public static implicit operator Image(VulkanSwapchainImage self) => self.Image;
}