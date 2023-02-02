using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Pipelines;

public class PipelineVertexInputStateCreateInformation : IGetCreateInfo<PipelineVertexInputStateCreateInfo> {
	public VertexInputAttributeDescription[] VertexAttributeDescriptions = Array.Empty<VertexInputAttributeDescription>();
	public VertexInputBindingDescription[] VertexBindingDescriptions = Array.Empty<VertexInputBindingDescription>();
	
	public unsafe ManagedResourceSet<PipelineVertexInputStateCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<PipelineVertexInputStateCreateInfo>(new PipelineVertexInputStateCreateInfo {
			SType = StructureType.PipelineVertexInputStateCreateInfo,
			VertexAttributeDescriptionCount = (uint)VertexAttributeDescriptions.Length,
			PVertexAttributeDescriptions = resources.AllocateArray(VertexAttributeDescriptions),
			VertexBindingDescriptionCount = (uint)VertexBindingDescriptions.Length,
			PVertexBindingDescriptions = resources.AllocateArray(VertexBindingDescriptions)
		}, resources);
	}
}