using System.Collections.Generic;
using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Descriptors; 

public static class DescriptorSetExtensions {
	public static DescriptorSet[] AllocateDescriptorSets(this Vk vk, Device device, DescriptorSetAllocateInformation allocInfo) {
		using var info = allocInfo.GetCreateInfo();
		var results = new DescriptorSet[allocInfo.SetLayouts.Length];
		vk.AllocateDescriptorSets(device, new[]{info.Resource}, results).AssertSuccess();
		return results;
	}

	public static void UpdateDescriptorSets(this Vk vk, Device device, 
											IEnumerable<WriteDescriptorSetInformation> writeInfos,
											IEnumerable<CopyDescriptorSetInformation> copyInfos) {
		using var wInfos = writeInfos.GetCreateInfos();
		using var cInfos = copyInfos.GetCreateInfos();
		vk.UpdateDescriptorSets(device, wInfos.Resources, cInfos.Resources);
	}
}