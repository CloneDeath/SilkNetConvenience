using System;
using System.Linq;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo.Descriptors;

namespace SilkNetConvenience.Wrappers; 

public class VulkanDescriptorPool : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly DescriptorPool DescriptorPool;
	
	public VulkanDescriptorPool(VulkanDevice device, DescriptorPoolCreateInformation createInfo) : this(device.Vk, device.Device, createInfo){}
	public VulkanDescriptorPool(Vk vk, Device device, DescriptorPoolCreateInformation createInfo) {
		Vk = vk;
		Device = device;
		DescriptorPool = vk.CreateDescriptorPool(device, createInfo);
	}
	protected override void ReleaseVulkanResources() {
		Vk.DestroyDescriptorPool(Device, DescriptorPool);
	}

	public VulkanDescriptorSet[] AllocateDescriptorSets(int count, VulkanDescriptorSetLayout layout) 
		=> AllocateDescriptorSets(count, layout.DescriptorSetLayout);
	public VulkanDescriptorSet[] AllocateDescriptorSets(int count, DescriptorSetLayout layout) {
		var layouts = new DescriptorSetLayout[count];
		Array.Fill(layouts, layout);
		return AllocateDescriptorSets(layouts);
	}
	public VulkanDescriptorSet[] AllocateDescriptorSets(params VulkanDescriptorSetLayout[] descriptorSetLayouts)
		=> AllocateDescriptorSets(descriptorSetLayouts.Select(d => d.DescriptorSetLayout).ToArray());
	public VulkanDescriptorSet[] AllocateDescriptorSets(params DescriptorSetLayout[] descriptorSetLayouts) {
		var results = Vk.AllocateDescriptorSets(Device, new DescriptorSetAllocateInformation {
			DescriptorPool = DescriptorPool,
			SetLayouts = descriptorSetLayouts
		});
		return results.Select(r => new VulkanDescriptorSet(this, r)).ToArray();
	}
}