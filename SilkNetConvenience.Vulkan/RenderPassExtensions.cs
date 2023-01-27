using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class RenderPassExtensions {
	public static RenderPass CreateRenderPass(this Vk vk, Device device, RenderPassCreateInformation createInfo) {
		var info = createInfo.GetCreateInfo();
		vk.CreateRenderPass(device, info, null, out var renderPass).AssertSuccess();
		return renderPass;
	}
	
	public static void DestroyRenderPass(this Vk vk, Device device, RenderPass renderPass) {
		vk.DestroyRenderPass(device, renderPass, null);
	}
}