using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Pipelines; 

public class GraphicsPipelineCreateInformation {
	public PipelineCreateFlags Flags;
	public PipelineLayout Layout;
	public uint Subpass;
	public PipelineShaderStageCreateInformation[] Stages = Array.Empty<PipelineShaderStageCreateInformation>();
	public RenderPass RenderPass;
	public Pipeline Pipeline;
	public int BasePipelineIndex;
	public PipelineDynamicStateCreateInformation DynamicState = new();
	public PipelineMultisampleStateCreateInformation MultisampleState = new();
	public PipelineRasterizationStateCreateInformation RasterizationState = new();
	public PipelineTessellationStateCreateInformation TessellationState = new();
	public PipelineViewportStateCreateInformation ViewportState = new();
	public PipelineColorBlendStateCreateInformation ColorBlendState = new();
	public PipelineDepthStencilStateCreateInformation DepthStencilState = new();
	public PipelineInputAssemblyStateCreateInformation InputAssemblyState = new();
	public PipelineVertexInputStateCreateInformation VertexInputState = new();

	public unsafe ManagedResourceSet<GraphicsPipelineCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<GraphicsPipelineCreateInfo>(new GraphicsPipelineCreateInfo {
			SType = StructureType.GraphicsPipelineCreateInfo,
			Flags = Flags,
			Layout = Layout,
			Subpass = Subpass,
			StageCount = (uint)Stages.Length,
			PStages = resources.AllocateCreateInfos<PipelineShaderStageCreateInfo, PipelineShaderStageCreateInformation>(Stages),
			RenderPass = RenderPass,
			BasePipelineHandle = Pipeline,
			BasePipelineIndex = BasePipelineIndex,
			PDynamicState = resources.AllocateCreateInfo(DynamicState),
			PMultisampleState = resources.AllocateCreateInfo(MultisampleState),
			PRasterizationState = resources.AllocateCreateInfo(RasterizationState),
			PTessellationState = resources.AllocateCreateInfo(TessellationState),
			PViewportState = resources.AllocateCreateInfo(ViewportState),
			PColorBlendState = resources.AllocateCreateInfo(ColorBlendState),
			PDepthStencilState = resources.AllocateCreateInfo(DepthStencilState),
			PInputAssemblyState = resources.AllocateCreateInfo(InputAssemblyState),
			PVertexInputState = resources.AllocateCreateInfo(VertexInputState)
		}, resources);
	}
}