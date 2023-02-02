using System.Linq;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Pipelines; 

public static unsafe class PipelineExtensions {
	public static Pipeline CreateGraphicsPipeline(this Vk vk, Device device, GraphicsPipelineCreateInformation createInfo) {
		using var info = createInfo.GetCreateInfo();
		vk.CreateGraphicsPipelines(device, default, 1, info.Resource, null, out var pipeline).AssertSuccess();
		return pipeline;
	}
	
	public static Pipeline[] CreateGraphicsPipelines(this Vk vk, Device device, GraphicsPipelineCreateInformation[] createInfo) {
		using var infos = createInfo.GetCreateInfos();
		var pipelines = new Pipeline[createInfo.Length];
		vk.CreateGraphicsPipelines(device, default, 1, infos.Resources, (AllocationCallbacks*)null, pipelines).AssertSuccess();
		return pipelines;
	}
	
	public static Pipeline CreateComputePipeline(this Vk vk, Device device, PipelineCache pipelineCache,
		ComputePipelineCreateInformation pipelineCreateInformation) {
		return vk.CreateComputePipelines(device, pipelineCache, new[] { pipelineCreateInformation }).First();
	}
	public static Pipeline[] CreateComputePipelines(this Vk vk, Device device, PipelineCache pipelineCache,
													ComputePipelineCreateInformation[] pipelineCreateInfos) {
		using var infos = pipelineCreateInfos.GetCreateInfos();
		var pipelines = new Pipeline[pipelineCreateInfos.Length];
		vk.CreateComputePipelines(device, pipelineCache, infos.Resources, null, pipelines).AssertSuccess();
		return pipelines;
	}
	
	public static void DestroyPipeline(this Vk vk, Device device, Pipeline pipeline) {
		vk.DestroyPipeline(device, pipeline, null);
	}
}