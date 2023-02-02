using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Pipelines;

public class PipelineDynamicStateCreateInformation : IGetCreateInfo<PipelineDynamicStateCreateInfo> {
	public DynamicState[] DynamicStates = Array.Empty<DynamicState>();
	
	public unsafe ManagedResourceSet<PipelineDynamicStateCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<PipelineDynamicStateCreateInfo>(new PipelineDynamicStateCreateInfo {
			SType = StructureType.PipelineDynamicStateCreateInfo,
			DynamicStateCount = (uint)DynamicStates.Length,
			PDynamicStates = resources.AllocateArray(DynamicStates)
		}, resources);
	}
}