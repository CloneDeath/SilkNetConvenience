using System.Collections.Generic;
using Silk.NET.Vulkan;
using SilkNetConvenience.Barriers;
using SilkNetConvenience.Exceptions;
using SilkNetConvenience.RenderPasses;

namespace SilkNetConvenience.CommandBuffers; 

public static class CommandBufferExtensions {
	public static CommandBuffer[] AllocateCommandBuffers(this Vk vk, Device device, CommandBufferAllocateInformation allocInfo) {
		using var info = allocInfo.GetCreateInfo();
		var infos = new[] { info.Resource };
		var commandBuffers = new CommandBuffer[allocInfo.CommandBufferCount];
		vk.AllocateCommandBuffers(device, infos, commandBuffers).AssertSuccess();
		return commandBuffers;
	}
	
	public static void BeginCommandBuffer(this Vk vk, CommandBuffer commandBuffer, CommandBufferBeginInformation beginInfo) {
		using var info = beginInfo.GetCreateInfo();
		vk.BeginCommandBuffer(commandBuffer, info.Resource).AssertSuccess();
	}

	public static void CmdBeginRenderPass(this Vk vk, CommandBuffer commandBuffer, RenderPassBeginInformation beginInfo, 
		SubpassContents subpassContents) {
		using var info = beginInfo.GetCreateInfo();
		vk.CmdBeginRenderPass(commandBuffer, info.Resource, subpassContents);
	}
	
	public static void CmdBindVertexBuffers(this Vk vk, CommandBuffer commandBuffer, uint firstBinding, uint bindingCount, 
		Buffer[] buffers, ulong[] offsets) {
		var bufferSpan = System.MemoryExtensions.AsSpan(buffers);
		var offsetSpan = System.MemoryExtensions.AsSpan(offsets);
		vk.CmdBindVertexBuffers(commandBuffer, firstBinding, bindingCount, bufferSpan, offsetSpan);
	}

	public static unsafe void CmdPipelineBarrier(this Vk vk, CommandBuffer commandBuffer, PipelineStageFlags srcStageMask,
		PipelineStageFlags dstStageMask, DependencyFlags dependencyFlags, 
		IEnumerable<MemoryBarrierInformation> memoryBarriers,
		IEnumerable<BufferMemoryBarrierInformation> bufferMemoryBarriers, 
		IEnumerable<ImageMemoryBarrierInformation> imageMemoryBarriers) {

		using var memory = memoryBarriers.GetCreateInfos();
		using var buffer = bufferMemoryBarriers.GetCreateInfos();
		using var image = imageMemoryBarriers.GetCreateInfos();
		vk.CmdPipelineBarrier(commandBuffer, srcStageMask, dstStageMask, dependencyFlags,
			memory.Length, memory.ResourcesPointer,
			buffer.Length, buffer.ResourcesPointer,
			image.Length, image.ResourcesPointer);
	}
}