using Silk.NET.Vulkan;

namespace SilkNetConvenience.Pipelines;

public class PipelineShaderStateCreateInformation : IGetCreateInfo<PipelineShaderStageCreateInfo> {
	public string Name = string.Empty;
	public ShaderStageFlags Stage;
	public PipelineShaderStageCreateFlags Flags;
	public ShaderModule Module;

	public unsafe ManagedResourceSet<PipelineShaderStageCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<PipelineShaderStageCreateInfo>(new PipelineShaderStageCreateInfo {
			SType = StructureType.PipelineShaderStageCreateInfo,
			Stage = Stage,
			Flags = Flags,
			Module = Module,
			PName = resources.AllocateString(Name)
		}, resources);
	}
}