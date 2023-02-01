using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo.Descriptors;

namespace SilkNetConvenience.Wrappers.Descriptors; 

public class VulkanDescriptorSetLayout : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly DescriptorSetLayout DescriptorSetLayout;

	public VulkanDescriptorSetLayout(VulkanDevice device, DescriptorSetLayoutCreateInformation createInfo)
		: this(device.Vk, device.Device, createInfo) {
		device.AddChildResource(this);
	}

	public VulkanDescriptorSetLayout(Vk vk, Device device, DescriptorSetLayoutCreateInformation createInfo) {
		Vk = vk;
		Device = device;
		DescriptorSetLayout = vk.CreateDescriptorSetLayout(device, createInfo);
	}
	
	protected override void ReleaseVulkanResources() {
		Vk.DestroyDescriptorSetLayout(Device, DescriptorSetLayout);
	}
	
	public static implicit operator DescriptorSetLayout(VulkanDescriptorSetLayout self) => self.DescriptorSetLayout;
}