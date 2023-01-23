using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo.Images;

namespace SilkNetConvenience.Wrappers; 

public class VulkanImageView : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly ImageView ImageView;
	
	public VulkanImageView(VulkanDevice device, ImageViewCreateInformation createInfo) : this(device.Vk, device.Device, createInfo){}
	public VulkanImageView(Vk vk, Device device, ImageViewCreateInformation createInfo) {
		Vk = vk;
		Device = device;
		ImageView = Vk.CreateImageView(Device, createInfo);
	}
	protected override void ReleaseVulkanResources() {
		Vk.DestroyImageView(Device, ImageView);
	}
}