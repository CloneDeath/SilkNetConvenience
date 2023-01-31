using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Pipelines;

public class ComputePipelineCreateInformation {
	public PipelineCreateFlags Flags;
	public PipelineLayout Layout;
	public PipelineShaderStateCreateInformation Stage = new();
	public Pipeline BasePipelineHandle;
	public int BasePipelineIndex;
}