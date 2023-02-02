using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Descriptors;

public class DescriptorPoolCreateInformation : IGetCreateInfo<DescriptorPoolCreateInfo> {
	public DescriptorPoolCreateFlags Flags;
	public uint MaxSets;
	public DescriptorPoolSize[] PoolSizes = Array.Empty<DescriptorPoolSize>();

	public unsafe ManagedResourceSet<DescriptorPoolCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<DescriptorPoolCreateInfo>(new DescriptorPoolCreateInfo {
			SType = StructureType.DescriptorPoolCreateInfo,
			Flags = Flags,
			MaxSets = MaxSets,
			PoolSizeCount = (uint)PoolSizes.Length,
			PPoolSizes = resources.AllocateArray(PoolSizes)
		}, resources);
	}
}