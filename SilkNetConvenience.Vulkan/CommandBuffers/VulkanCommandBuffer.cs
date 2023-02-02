using System;
using System.Linq;
using Silk.NET.Vulkan;
using SilkNetConvenience.Barriers;
using SilkNetConvenience.Buffers;
using SilkNetConvenience.Descriptors;
using SilkNetConvenience.Exceptions;
using SilkNetConvenience.Images;
using SilkNetConvenience.Pipelines;
using SilkNetConvenience.RenderPasses;
using Buffer = Silk.NET.Vulkan.Buffer;

namespace SilkNetConvenience.CommandBuffers; 

public class VulkanCommandBuffer : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly CommandPool CommandPool;
	public readonly CommandBuffer CommandBuffer;

	public VulkanCommandBuffer(Vk vk, Device device, CommandPool commandPool, CommandBuffer commandBuffer) {
		Vk = vk;
		Device = device;
		CommandPool = commandPool;
		CommandBuffer = commandBuffer;
	}

	public VulkanCommandBuffer(VulkanCommandPool commandPool, CommandBufferLevel level)
		: this(commandPool.Vk, commandPool.Device, commandPool.CommandPool, level) {
		commandPool.AddChildResource(this);
	}
	public VulkanCommandBuffer(Vk vk, Device device, CommandPool commandPool, CommandBufferLevel level) {
		Vk = vk;
		Device = device;
		CommandPool = commandPool;
		CommandBuffer = vk.AllocateCommandBuffers(device, new CommandBufferAllocateInformation {
			Level = level,
			CommandBufferCount = 1,
			CommandPool = commandPool
		})[0];
	}

	protected override void ReleaseVulkanResources() {
		Vk.FreeCommandBuffers(Device, CommandPool, 1, CommandBuffer);
	}
	
	public static implicit operator CommandBuffer(VulkanCommandBuffer self) => self.CommandBuffer;

	public void Begin(CommandBufferUsageFlags usage = CommandBufferUsageFlags.None) {
		Vk.BeginCommandBuffer(CommandBuffer, new CommandBufferBeginInformation {
			Flags = usage
		});
	}

	public void CopyBuffer(VulkanBuffer source, VulkanBuffer destination, ulong size)
		=> CopyBuffer(source, destination, new BufferCopy { Size = size });
	public void CopyBuffer(VulkanBuffer source, VulkanBuffer destination, params BufferCopy[] copies) {
		Vk.CmdCopyBuffer(CommandBuffer, source.Buffer, destination.Buffer, copies);
	}

	public void End() {
		Vk.EndCommandBuffer(CommandBuffer).AssertSuccess();
	}

	public void Reset(CommandBufferResetFlags resetFlags = CommandBufferResetFlags.None) {
		Vk.ResetCommandBuffer(CommandBuffer, resetFlags).AssertSuccess();
	}

	public void BeginRenderPass(RenderPassBeginInformation beginInfo, SubpassContents subpassContents) {
		Vk.CmdBeginRenderPass(CommandBuffer, beginInfo, subpassContents);
	}

	public void BindPipeline(VulkanPipeline pipeline) {
		BindPipeline(pipeline.PipelineBindPoint, pipeline.Pipeline);
	}
	public void BindPipeline(PipelineBindPoint pipelineBindPoint, Pipeline pipeline) {
		Vk.CmdBindPipeline(CommandBuffer, pipelineBindPoint, pipeline);
	}

	public void BindVertexBuffers(uint firstBinding, uint bindingCount, Buffer[] buffers, ulong[]? offsets = null) {
		var actualOffsets = offsets ?? new ulong[buffers.Length];
		Vk.CmdBindVertexBuffers(CommandBuffer, firstBinding, bindingCount, buffers, actualOffsets);
	}

	public void BindVertexBuffer(uint binding, Buffer buffer, ulong offset = 0) {
		var buffers = new[] { buffer };
		var offsets = new[] { offset };
		Vk.CmdBindVertexBuffers(CommandBuffer, binding, buffers, offsets);
	}

	public void BindIndexBuffer(VulkanBuffer indexBuffer, ulong offset, IndexType indexType)
		=> BindIndexBuffer(indexBuffer.Buffer, offset, indexType);
	public void BindIndexBuffer(Buffer indexBuffer, ulong offset, IndexType indexType) {
		Vk.CmdBindIndexBuffer(CommandBuffer, indexBuffer, offset, indexType);
	}

	public void SetViewport(uint firstViewport, params Viewport[] viewports) {
		Vk.CmdSetViewport(CommandBuffer, firstViewport, (uint)viewports.Length, viewports);
	}
	
	public void SetScissor(uint firstScissor, params Rect2D[] scissor) {
		Vk.CmdSetScissor(CommandBuffer, firstScissor, (uint)scissor.Length, scissor);
	}

	public void Draw(uint vertexCount, uint instanceCount = 1, uint firstVertex = 0, uint firstInstance = 0) {
		Vk.CmdDraw(CommandBuffer, vertexCount, instanceCount, firstVertex, firstInstance);
	}
	
	public void DrawIndexed(uint indexCount, uint instanceCount = 1, uint firstIndex = 0, int vertexOffset = 0, uint firstInstance = 0) {
		Vk.CmdDrawIndexed(CommandBuffer, indexCount, instanceCount, firstIndex, vertexOffset, firstInstance);
	}

	public void EndRenderPass() => Vk.CmdEndRenderPass(CommandBuffer);

	public void BindDescriptorSet(PipelineBindPoint bindPoint, PipelineLayout pipelineLayout, uint firstSet,
		VulkanDescriptorSet descriptorSet, uint? dynamicOffset = null) {
		var offsets = dynamicOffset.HasValue ? new[] { dynamicOffset.Value } : Array.Empty<uint>();
		BindDescriptorSets(bindPoint, pipelineLayout, firstSet, new[] { descriptorSet }, offsets);
	}
	public void BindDescriptorSets(PipelineBindPoint pipelineBindPoint, PipelineLayout pipelineLayout, uint firstSet, 
			VulkanDescriptorSet[] descriptorSets, uint[]? dynamicOffsets = null) {
		var sets = descriptorSets.Select(s => s.DescriptorSet).ToArray();
		BindDescriptorSets(pipelineBindPoint, pipelineLayout, firstSet, sets, dynamicOffsets);
	}
	public void BindDescriptorSets(PipelineBindPoint pipelineBindPoint, PipelineLayout pipelineLayout, uint firstSet, 
			DescriptorSet[] descriptorSets, uint[]? dynamicOffsets = null) {
		var offsets = dynamicOffsets ?? Array.Empty<uint>();
		Vk.CmdBindDescriptorSets(CommandBuffer, pipelineBindPoint, pipelineLayout, firstSet, descriptorSets, offsets);
	}

	public void PipelineBarrier(PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, 
		DependencyFlags dependencyFlags, params MemoryBarrierInformation[] memoryBarriers) {
		PipelineBarrier(srcStageMask, dstStageMask, dependencyFlags, memoryBarriers,
			Array.Empty<BufferMemoryBarrierInformation>(), Array.Empty<ImageMemoryBarrierInformation>());
	}
	
	public void PipelineBarrier(PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, 
		DependencyFlags dependencyFlags, params BufferMemoryBarrierInformation[] bufferMemoryBarriers) {
		PipelineBarrier(srcStageMask, dstStageMask, dependencyFlags, Array.Empty<MemoryBarrierInformation>(),
			bufferMemoryBarriers, Array.Empty<ImageMemoryBarrierInformation>());
	}
	
	public void PipelineBarrier(PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, 
		DependencyFlags dependencyFlags, params ImageMemoryBarrierInformation[] imageMemoryBarriers) {
		PipelineBarrier(srcStageMask, dstStageMask, dependencyFlags, Array.Empty<MemoryBarrierInformation>(),
			Array.Empty<BufferMemoryBarrierInformation>(), imageMemoryBarriers);
	}
	
	public void PipelineBarrier(PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, 
		DependencyFlags dependencyFlags, MemoryBarrierInformation[] memoryBarriers,
		BufferMemoryBarrierInformation[] bufferMemoryBarriers, ImageMemoryBarrierInformation[] imageMemoryBarriers) {
		Vk.CmdPipelineBarrier(CommandBuffer, srcStageMask, dstStageMask, dependencyFlags, memoryBarriers,
			bufferMemoryBarriers, imageMemoryBarriers);
	}

	public void CopyBufferToImage(VulkanBuffer buffer, VulkanImage image, ImageLayout imageLayout, params BufferImageCopy[] regions) {
		CopyBufferToImage(buffer.Buffer, image.Image, imageLayout, regions);
	}
	
	public void CopyBufferToImage(Buffer buffer, Image image, ImageLayout imageLayout, params BufferImageCopy[] regions) {
		Vk.CmdCopyBufferToImage(CommandBuffer, buffer, image, imageLayout, (uint)regions.Length, regions);
	}
}