using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Pipelines;

public class PipelineDepthStencilStateCreateInformation : IGetCreateInfo<PipelineDepthStencilStateCreateInfo> {
	public StencilOpState Back;
	public StencilOpState Front;
	public PipelineDepthStencilStateCreateFlags Flags;
	public CompareOp DepthCompareOp;
	public bool DepthTestEnable;
	public bool DepthWriteEnable;
	public float MaxDepthBounds;
	public float MinDepthBounds;
	public bool StencilTestEnable;
	public bool DepthBoundsTestEnable;

	public ManagedResourceSet<PipelineDepthStencilStateCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<PipelineDepthStencilStateCreateInfo>(new PipelineDepthStencilStateCreateInfo {
			SType = StructureType.PipelineDepthStencilStateCreateInfo,
			Back = Back,
			Front = Front,
			Flags = Flags,
			DepthCompareOp = DepthCompareOp,
			DepthTestEnable = DepthTestEnable,
			DepthWriteEnable = DepthWriteEnable,
			MaxDepthBounds = MaxDepthBounds,
			MinDepthBounds = MinDepthBounds,
			StencilTestEnable = StencilTestEnable,
			DepthBoundsTestEnable = DepthBoundsTestEnable
		}, resources);
	}
}