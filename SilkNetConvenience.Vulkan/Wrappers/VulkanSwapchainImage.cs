using Silk.NET.Vulkan;

namespace SilkNetConvenience.Wrappers; 

public class VulkanSwapchainImage : VulkanImage {
	public VulkanSwapchainImage(VulkanDevice device, Image image) : base(device, image) { }
	public VulkanSwapchainImage(Vk vk, Device device, Image image) : base(vk, device, image) { }

	protected override void ReleaseVulkanResources() { }
}