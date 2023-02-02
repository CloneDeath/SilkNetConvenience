using Silk.NET.Vulkan;

namespace SilkNetConvenience.Pipelines;

public class ComputePipelineCreateInformation : IGetCreateInfo<ComputePipelineCreateInfo> {
	public PipelineCreateFlags Flags;
	public PipelineLayout Layout;
	public PipelineShaderStateCreateInformation Stage = new();
	public Pipeline BasePipelineHandle;
	public int BasePipelineIndex;

	public unsafe ManagedResourceSet<ComputePipelineCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<ComputePipelineCreateInfo>(new ComputePipelineCreateInfo {
			SType = StructureType.ComputePipelineCreateInfo,
			Flags = Flags,
			Layout = Layout,
			Stage = *resources.AllocateCreateInfo(Stage),
			BasePipelineHandle = BasePipelineHandle,
			BasePipelineIndex = BasePipelineIndex
		}, resources);
	}
}