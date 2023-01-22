using System.Linq;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.CreateInfo.Barriers;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static class CommandBufferExtensions {
	public static void BeginCommandBuffer(this Vk vk, CommandBuffer commandBuffer, CommandBufferBeginInformation beginInfo) {
		var info = beginInfo.GetBeginInfo();
		vk.BeginCommandBuffer(commandBuffer, info).AssertSuccess();
	}

	public static void CmdBeginRenderPass(this Vk vk, CommandBuffer commandBuffer, RenderPassBeginInformation beginInfo, 
		SubpassContents subpassContents) {
		var info = beginInfo.GetBeginInfo();
		vk.CmdBeginRenderPass(commandBuffer, info, subpassContents);
	}
	
	public static void CmdBindVertexBuffers(this Vk vk, CommandBuffer commandBuffer, uint firstBinding, uint bindingCount, 
		Buffer[] buffers, ulong[] offsets) {
		var bufferSpan = System.MemoryExtensions.AsSpan(buffers);
		var offsetSpan = System.MemoryExtensions.AsSpan(offsets);
		vk.CmdBindVertexBuffers(commandBuffer, firstBinding, bindingCount, bufferSpan, offsetSpan);
	}

	public static void CmdPipelineBarrier(this Vk vk, CommandBuffer commandBuffer, PipelineStageFlags srcStageMask,
		PipelineStageFlags dstStageMask, DependencyFlags dependencyFlags, MemoryBarrierInformation[] memoryBarriers,
		BufferMemoryBarrierInformation[] bufferMemoryBarriers, ImageMemoryBarrierInformation[] imageMemoryBarriers) {

		var memory = memoryBarriers.Select(m => m.GetBarrier()).ToArray();
		var buffer = bufferMemoryBarriers.Select(b => b.GetBarrier()).ToArray();
		var image = imageMemoryBarriers.Select(i => i.GetBarrier()).ToArray();
		vk.CmdPipelineBarrier(commandBuffer, srcStageMask, dstStageMask, dependencyFlags,
			(uint)memory.Length, memory,
			(uint)buffer.Length, buffer,
			(uint)image.Length, image);
	}
}