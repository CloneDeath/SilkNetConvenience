using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Pipelines;

public class PipelineInputAssemblyStateCreateInformation : IGetCreateInfo<PipelineInputAssemblyStateCreateInfo> {
	public PrimitiveTopology Topology;
	public bool PrimitiveRestartEnable;

	public ManagedResourceSet<PipelineInputAssemblyStateCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<PipelineInputAssemblyStateCreateInfo>(new PipelineInputAssemblyStateCreateInfo {
			SType = StructureType.PipelineInputAssemblyStateCreateInfo,
			Topology = Topology,
			PrimitiveRestartEnable = PrimitiveRestartEnable
		}, resources);
	}
}