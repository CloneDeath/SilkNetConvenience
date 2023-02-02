using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Descriptors;

public class DescriptorSetAllocateInformation : IGetCreateInfo<DescriptorSetAllocateInfo> {
	public DescriptorPool DescriptorPool;
	public DescriptorSetLayout[] SetLayouts = Array.Empty<DescriptorSetLayout>();

	public unsafe ManagedResourceSet<DescriptorSetAllocateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<DescriptorSetAllocateInfo>(new DescriptorSetAllocateInfo {
			SType = StructureType.DescriptorSetAllocateInfo,
			DescriptorPool = DescriptorPool,
			DescriptorSetCount = (uint)SetLayouts.Length,
			PSetLayouts = resources.AllocateArray(SetLayouts)
		}, resources);
	}
}