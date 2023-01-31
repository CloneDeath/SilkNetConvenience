using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Pipelines;

public class PipelineTessellationStateCreateInformation : IGetCreateInfo<PipelineTessellationStateCreateInfo> {
	public uint PatchControlPoints;

	public ManagedResourceSet<PipelineTessellationStateCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<PipelineTessellationStateCreateInfo>(new PipelineTessellationStateCreateInfo {
			SType = StructureType.PipelineTessellationStateCreateInfo,
			PatchControlPoints = PatchControlPoints
		}, resources);
	}
}