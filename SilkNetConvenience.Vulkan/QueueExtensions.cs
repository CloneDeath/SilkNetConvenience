using System.Linq;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class QueueExtensions {
	public static Queue GetDeviceQueue(this Vk vk, Device device, uint queueFamilyIndex, uint queueIndex) {
		vk.GetDeviceQueue(device, queueFamilyIndex, queueIndex, out var queue);
		return queue;
	}
	
	public static void QueueSubmit(this Vk vk, Queue queue, SubmitInformation[] submitInfos, Fence fence) {
		var infos = submitInfos.Select(s => {
			fixed (CommandBuffer* buffers = s.CommandBuffers)
			fixed (Semaphore* signalSemaphores = s.SignalSemaphores)
			fixed (Semaphore* waitSemaphores = s.WaitSemaphores)
			fixed (PipelineStageFlags* waitDstStageMasks = s.WaitDstStageMask) {
				return new SubmitInfo {
					SType = StructureType.SubmitInfo,
					CommandBufferCount = (uint)s.CommandBuffers.Length,
					PCommandBuffers = buffers,
					SignalSemaphoreCount = (uint)s.SignalSemaphores.Length,
					PSignalSemaphores = signalSemaphores,
					WaitSemaphoreCount = (uint)s.WaitSemaphores.Length,
					PWaitSemaphores = waitSemaphores,
					PWaitDstStageMask = waitDstStageMasks
				};
			}
		}).ToArray();
		vk.QueueSubmit(queue, infos, fence).AssertSuccess();
	}
}