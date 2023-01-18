using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static class CommandBufferExtensions {
	public static void BeginCommandBuffer(this Vk vk, CommandBuffer commandBuffer, CommandBufferBeginInformation beginInfo) {
		var info = beginInfo.GetBeginInfo();
		vk.BeginCommandBuffer(commandBuffer, info).AssertSuccess();
	}
}