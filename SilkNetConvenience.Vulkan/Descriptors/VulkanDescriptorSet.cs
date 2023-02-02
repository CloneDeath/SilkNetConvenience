using System.Linq;
using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Descriptors; 

public class VulkanDescriptorSet : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly DescriptorPool DescriptorPool;
	public readonly DescriptorSet DescriptorSet;
	public readonly bool CanFreeDescriptorSet;

	public VulkanDescriptorSet(VulkanDescriptorPool descriptorPool, DescriptorSet descriptorSet) {
		Vk = descriptorPool.Vk;
		Device = descriptorPool.Device;
		DescriptorPool = descriptorPool.DescriptorPool;
		DescriptorSet = descriptorSet;
		CanFreeDescriptorSet = descriptorPool.CanFreeDescriptorSets;
		descriptorPool.AddChildResource(this);
	}

	public VulkanDescriptorSet(VulkanDescriptorPool descriptorPool, DescriptorSetLayout layout)
		: this(descriptorPool.Vk, descriptorPool.Device, descriptorPool.DescriptorPool, layout, descriptorPool.CanFreeDescriptorSets) {
		descriptorPool.AddChildResource(this);
	} 
	public VulkanDescriptorSet(Vk vk, Device device, DescriptorPool descriptorPool, DescriptorSetLayout layout, bool canFreeDescriptorSet) {
		Vk = vk;
		Device = device;
		DescriptorPool = descriptorPool;
		CanFreeDescriptorSet = canFreeDescriptorSet;
		DescriptorSet = vk.AllocateDescriptorSets(Device, new DescriptorSetAllocateInformation {
			DescriptorPool = descriptorPool,
			SetLayouts = new[]{layout}
		}).First();
	}

	protected override void ReleaseVulkanResources() {
		if (!CanFreeDescriptorSet) return;
		Vk.FreeDescriptorSets(Device, DescriptorPool, 1, DescriptorSet).AssertSuccess();
	}
	
	public static implicit operator DescriptorSet(VulkanDescriptorSet self) => self.DescriptorSet;
}