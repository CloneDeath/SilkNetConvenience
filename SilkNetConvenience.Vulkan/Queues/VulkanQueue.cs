using System;
using System.Collections.Generic;
using Silk.NET.Vulkan;
using SilkNetConvenience.CommandBuffers;
using SilkNetConvenience.Devices;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Queues;

public class VulkanQueue {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly Queue Queue;
	
	public VulkanQueue(VulkanDevice device, uint queueFamilyIndex, uint queueIndex)
		: this(device.Vk, device.Device, queueFamilyIndex, queueIndex){ }
	public VulkanQueue(Vk vk, Device device, uint queueFamilyIndex, uint queueIndex) {
		Vk = vk;
		Device = device;
		vk.GetDeviceQueue(device, queueFamilyIndex, queueIndex, out Queue);
	}
	
	public static implicit operator Queue(VulkanQueue self) => self.Queue;

	public void Submit(SubmitInformation submitInfo, Fence fence = default) {
		Submit(new[]{submitInfo}, fence);
	}
	public void Submit(IEnumerable<SubmitInformation> submitInfos, Fence fence = default) {
		Vk.QueueSubmit(Queue, submitInfos, fence);
	}
	public void Submit(params CommandBuffer[] commandBuffers) {
		Vk.QueueSubmit(Queue,  new SubmitInformation { CommandBuffers = commandBuffers });
	}

	public void WaitIdle() {
		Vk.QueueWaitIdle(Queue).AssertSuccess();
	}

	public void SubmitSingleUseCommandBufferAndWaitIdle(VulkanCommandPool commandPool, Action<VulkanCommandBuffer> commands) {
		using var buffer = commandPool.AllocateCommandBuffer();
		buffer.Begin(CommandBufferUsageFlags.OneTimeSubmitBit);
		commands(buffer);
		buffer.End();

		Submit(buffer);
		WaitIdle();
	}
}