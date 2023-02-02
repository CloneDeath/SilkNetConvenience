using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Descriptors;

public class DescriptorSetLayoutCreateInformation : IGetCreateInfo<DescriptorSetLayoutCreateInfo> {
	public DescriptorSetLayoutCreateFlags Flags;
	public DescriptorSetLayoutBindingInformation[] Bindings = Array.Empty<DescriptorSetLayoutBindingInformation>();

	public unsafe ManagedResourceSet<DescriptorSetLayoutCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<DescriptorSetLayoutCreateInfo>(new DescriptorSetLayoutCreateInfo {
			SType = StructureType.DescriptorSetLayoutCreateInfo,
			Flags = Flags,
			BindingCount = (uint)Bindings.Length,
			PBindings = resources.AllocateCreateInfos(Bindings)
		}, resources);
	}
}