using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Pipelines;

public class PipelineMultisampleStateCreateInformation : IGetCreateInfo<PipelineMultisampleStateCreateInfo> {
	public SampleCountFlags RasterizationSamples;
	public float MinSampleShading;
	public uint[] SampleMask = Array.Empty<uint>();
	public bool SampleShadingEnable;
	public bool AlphaToCoverageEnable;
	public bool AlphaToOneEnable;

	public unsafe ManagedResourceSet<PipelineMultisampleStateCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<PipelineMultisampleStateCreateInfo>(new PipelineMultisampleStateCreateInfo {
			SType = StructureType.PipelineMultisampleStateCreateInfo,
			RasterizationSamples = RasterizationSamples,
			MinSampleShading = MinSampleShading,
			PSampleMask = resources.AllocateArray(SampleMask),
			SampleShadingEnable = SampleShadingEnable,
			AlphaToCoverageEnable = AlphaToCoverageEnable,
			AlphaToOneEnable = AlphaToOneEnable 
		}, resources);
	}
}