using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo;

public class PipelineShaderStateCreateInformation {
	public string Name = string.Empty;
	public ShaderStageFlags Stage;
	public PipelineShaderStageCreateFlags Flags;
	public ShaderModule Module;
}