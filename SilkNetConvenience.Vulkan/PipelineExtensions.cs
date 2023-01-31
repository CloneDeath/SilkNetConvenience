using System.Linq;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo.Pipelines;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class PipelineExtensions {
	public static Pipeline CreateGraphicsPipeline(this Vk vk, Device device, GraphicsPipelineCreateInformation createInfo) {
		using var info = createInfo.GetCreateInfo();
		vk.CreateGraphicsPipelines(device, default, 1, info.Resource, null, out var pipeline).AssertSuccess();
		return pipeline;
	}
	
	public static Pipeline[] CreateGraphicsPipelines(this Vk vk, Device device, GraphicsPipelineCreateInformation[] createInfo) {
		var resources = createInfo.Select(c => c.GetCreateInfo()).ToArray();
		var infos = resources.Select(r => r.Resource).ToArray();
		try {
			var pipelines = new Pipeline[createInfo.Length];
			vk.CreateGraphicsPipelines(device, default, 1, infos, (AllocationCallbacks*)null, pipelines).AssertSuccess();
			return pipelines;
		}
		finally {
			foreach (var managedResourceSet in resources) {
				managedResourceSet.Dispose();
			}
		}
	}
	
	public static Pipeline CreateComputePipeline(this Vk vk, Device device, PipelineCache pipelineCache,
		ComputePipelineCreateInformation pipelineCreateInformation) {
		return vk.CreateComputePipelines(device, pipelineCache, new[] { pipelineCreateInformation }).First();
	}

	public static Pipeline[] CreateComputePipelines(this Vk vk, Device device, PipelineCache pipelineCache,
		ComputePipelineCreateInformation[] pipelineCreateInfos) {
		var createInfos = pipelineCreateInfos.Select(p => new ComputePipelineCreateInfo {
			SType = StructureType.ComputePipelineCreateInfo,
			Flags = p.Flags,
			Layout = p.Layout,
			Stage = new PipelineShaderStageCreateInfo {
				SType = StructureType.PipelineShaderStageCreateInfo,
				Stage = p.Stage.Stage,
				Flags = p.Stage.Flags,
				Module = p.Stage.Module,
				PName = (byte*)SilkMarshal.StringToPtr(p.Stage.Name)
			},
			BasePipelineHandle = p.BasePipelineHandle,
			BasePipelineIndex = p.BasePipelineIndex
		}).ToArray();

		var pipelines = new Pipeline[pipelineCreateInfos.Length];

		try {
			vk.CreateComputePipelines(device, pipelineCache, createInfos, null, pipelines).AssertSuccess();
			return pipelines;
		}
		finally {
			foreach (var createInfo in createInfos) {
				SilkMarshal.Free((nint)createInfo.Stage.PName);
			}
		}
	}
}