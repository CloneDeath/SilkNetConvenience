using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo;

public class ComputePipelineCreateInformation {
	public PipelineCreateFlags Flags;
	public PipelineLayout Layout;
	public PipelineShaderStateCreateInformation Stage = new();
	public Pipeline BasePipelineHandle;
	public int BasePipelineIndex;
}

public class PipelineShaderStateCreateInformation {
	public string Name = string.Empty;
	public ShaderStageFlags Stage;
	public PipelineShaderStageCreateFlags Flags;
	public ShaderModule Module;
}
