using Silk.NET.Core;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Pipelines;

public class PipelineRasterizationStateCreateInformation : IGetCreateInfo<PipelineRasterizationStateCreateInfo> {
	public CullModeFlags CullMode;
	public FrontFace FrontFace;
	public float LineWidth;
	public PolygonMode PolygonMode;
	public float DepthBiasClamp;
	public Bool32 DepthBiasEnable;
	public Bool32 DepthClampEnable;
	public Bool32 RasterizerDiscardEnable;
	public float DepthBiasConstantFactor;
	public float DepthBiasSlopeFactor;

	public ManagedResourceSet<PipelineRasterizationStateCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<PipelineRasterizationStateCreateInfo>(new PipelineRasterizationStateCreateInfo {
			SType = StructureType.PipelineRasterizationStateCreateInfo,
			CullMode = CullMode,
			FrontFace = FrontFace,
			LineWidth = LineWidth,
			PolygonMode = PolygonMode,
			DepthBiasClamp = DepthBiasClamp,
			DepthBiasEnable = DepthBiasEnable,
			DepthClampEnable = DepthClampEnable,
			RasterizerDiscardEnable = RasterizerDiscardEnable,
			DepthBiasConstantFactor = DepthBiasConstantFactor,
			DepthBiasSlopeFactor = DepthBiasSlopeFactor
		}, resources);
	}
}