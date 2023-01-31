using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Pipelines;

public class PipelineShaderStageCreateInformation : IGetCreateInfo<PipelineShaderStageCreateInfo> {
	public PipelineShaderStageCreateFlags Flags;
	public ShaderModule Module;
	public ShaderStageFlags Stage;
	public string Name = string.Empty;

	public unsafe ManagedResourceSet<PipelineShaderStageCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<PipelineShaderStageCreateInfo>(new PipelineShaderStageCreateInfo {
			SType = StructureType.PipelineShaderStageCreateInfo,
			Flags = Flags,
			Module = Module,
			Stage = Stage,
			PName = resources.AllocateString(Name)
		}, resources);
	}
}