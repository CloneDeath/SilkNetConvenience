using Silk.NET.Vulkan;
using SilkNetConvenience.Devices;

namespace SilkNetConvenience.Images; 

public class VulkanImageView : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly ImageView ImageView;

	public VulkanImageView(VulkanDevice device, ImageViewCreateInformation createInfo) : this(device.Vk, device.Device,
		createInfo) {
		device.AddChildResource(this);
	}
	public VulkanImageView(Vk vk, Device device, ImageViewCreateInformation createInfo) {
		Vk = vk;
		Device = device;
		ImageView = Vk.CreateImageView(Device, createInfo);
	}
	protected override void ReleaseVulkanResources() {
		Vk.DestroyImageView(Device, ImageView);
	}
	
	public static implicit operator ImageView(VulkanImageView self) => self.ImageView;
}