using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Pipelines;

public class PipelineLayoutCreateInformation : IGetCreateInfo<PipelineLayoutCreateInfo> {
	public PipelineLayoutCreateFlags Flags;
	public DescriptorSetLayout[] SetLayouts = Array.Empty<DescriptorSetLayout>();
	public PushConstantRange[] PushConstantRanges = Array.Empty<PushConstantRange>();

	public unsafe ManagedResourceSet<PipelineLayoutCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<PipelineLayoutCreateInfo>(new PipelineLayoutCreateInfo {
			SType = StructureType.PipelineLayoutCreateInfo,
			Flags = Flags,
			SetLayoutCount = (uint)SetLayouts.Length,
			PSetLayouts = resources.AllocateArray(SetLayouts),
			PushConstantRangeCount = (uint)PushConstantRanges.Length,
			PPushConstantRanges = resources.AllocateArray(PushConstantRanges)
		}, resources);
	}
}