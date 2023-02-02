using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Pipelines; 

public static unsafe class PipelineLayoutExtensions {
	public static PipelineLayout CreatePipelineLayout(this Vk vk, Device device, PipelineLayoutCreateInformation createInfo) {
		using var info = createInfo.GetCreateInfo();
		vk.CreatePipelineLayout(device, info.Resource, null, out var pipelineLayout).AssertSuccess();
		return pipelineLayout;
	}

	public static void DestroyPipelineLayout(this Vk vk, Device device, PipelineLayout layout) {
		vk.DestroyPipelineLayout(device, layout, null);
	}
}