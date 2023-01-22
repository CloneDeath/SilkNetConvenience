using System.Linq;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Wrappers; 

public class VulkanDescriptorSet : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly DescriptorPool DescriptorPool;
	public readonly DescriptorSet DescriptorSet;

	public VulkanDescriptorSet(VulkanDescriptorPool descriptorPool, DescriptorSet descriptorSet) {
		Vk = descriptorPool.Vk;
		Device = descriptorPool.Device;
		DescriptorPool = descriptorPool.DescriptorPool;
		DescriptorSet = descriptorSet;
	}
	public VulkanDescriptorSet(VulkanDescriptorPool descriptorPool, DescriptorSetLayout layout) 
		: this(descriptorPool.Vk, descriptorPool.Device, descriptorPool.DescriptorPool, layout){} 
	public VulkanDescriptorSet(Vk vk, Device device, DescriptorPool descriptorPool, DescriptorSetLayout layout) {
		Vk = vk;
		Device = device;
		DescriptorPool = descriptorPool;
		DescriptorSet = vk.AllocateDescriptorSets(Device, new DescriptorSetAllocateInformation {
			DescriptorPool = descriptorPool,
			SetLayouts = new[]{layout}
		}).First();
	}

	protected override void ReleaseVulkanResources() {
		Vk.FreeDescriptorSets(Device, DescriptorPool, 1, DescriptorSet).AssertSuccess();
	}
}