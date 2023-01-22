using System;
using System.Linq;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Wrappers;

public class VulkanQueue {
	public readonly Vk Vk;
	public readonly Queue Queue;
	
	public VulkanQueue(VulkanDevice device, uint queueFamilyIndex, uint queueIndex)
		: this(device.Vk, device.Device, queueFamilyIndex, queueIndex){ }
	public VulkanQueue(Vk vk, Device device, uint queueFamilyIndex, uint queueIndex) {
		Vk = vk;
		vk.GetDeviceQueue(device, queueFamilyIndex, queueIndex, out Queue);
	}

	public void Submit(params VulkanCommandBuffer[] commandBuffers) {
		Vk.QueueSubmit(Queue, new SubmitInformation[] {
			new() {
				CommandBuffers = commandBuffers.Select(b => b.CommandBuffer).ToArray()
			}
		}, default);
	}

	public void WaitIdle() {
		Vk.QueueWaitIdle(Queue).AssertSuccess();
	}
	
	public void SubmitSingleUseCommandBufferAndWaitIdle(VulkanCommandPool commandPool, Action<VulkanCommandBuffer> commands) {
		using var buffer = commandPool.AllocateCommandBuffer(CommandBufferLevel.Primary);
		buffer.Begin(CommandBufferUsageFlags.OneTimeSubmitBit);
		commands(buffer);
		buffer.End();

		Submit(buffer);
		WaitIdle();
	}
}