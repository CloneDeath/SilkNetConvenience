using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class CommandBufferExtensions {
	public static void BeginCommandBuffer(this Vk vk, CommandBuffer commandBuffer, CommandBufferBeginInformation beginInfo) {
		var info = beginInfo.GetBeginInfo();
		vk.BeginCommandBuffer(commandBuffer, info).AssertSuccess();
	}

	public static void CmdBeginRenderPass(this Vk vk, CommandBuffer commandBuffer, RenderPassBeginInformation beginInfo, 
		SubpassContents subpassContents) {
		var info = beginInfo.GetBeginInfo();
		vk.CmdBeginRenderPass(commandBuffer, info, subpassContents);
	}
	
	public static void BindVertexBuffers(this Vk vk, CommandBuffer commandBuffer, uint firstBinding, uint bindingCount, 
		Buffer[] buffers, ulong[] offsets) {
		var bufferSpan = System.MemoryExtensions.AsSpan(buffers);
		var offsetSpan = System.MemoryExtensions.AsSpan(offsets);
		vk.CmdBindVertexBuffers(commandBuffer, firstBinding, bindingCount, bufferSpan, offsetSpan);
	}
}