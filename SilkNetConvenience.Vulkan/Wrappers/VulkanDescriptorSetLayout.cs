using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo.Descriptors;

namespace SilkNetConvenience.Wrappers; 

public class VulkanDescriptorSetLayout : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly DescriptorSetLayout DescriptorSetLayout;

	public VulkanDescriptorSetLayout(VulkanDevice device, DescriptorSetLayoutCreateInformation createInfo) 
		: this(device.Vk, device.Device, createInfo) { }

	public VulkanDescriptorSetLayout(Vk vk, Device device, DescriptorSetLayoutCreateInformation createInfo) {
		Vk = vk;
		Device = device;
		DescriptorSetLayout = vk.CreateDescriptorSetLayout(device, createInfo);
	}
	
	protected override void ReleaseVulkanResources() {
		Vk.DestroyDescriptorSetLayout(Device, DescriptorSetLayout);
	}
}