using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo;

public class PipelineLayoutCreateInformation {
	public PipelineLayoutCreateFlags Flags;
	public DescriptorSetLayout[] SetLayouts = Array.Empty<DescriptorSetLayout>();
	public PushConstantRange[] PushConstantRanges = Array.Empty<PushConstantRange>();
}