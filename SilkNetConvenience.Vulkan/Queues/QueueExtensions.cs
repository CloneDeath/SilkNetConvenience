using System.Collections.Generic;
using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Queues; 

public static class QueueExtensions {
	public static Queue GetDeviceQueue(this Vk vk, Device device, uint queueFamilyIndex, uint queueIndex) {
		vk.GetDeviceQueue(device, queueFamilyIndex, queueIndex, out var queue);
		return queue;
	}

	public static void QueueSubmit(this Vk vk, Queue queue, params SubmitInformation[] submitInfos) {
		vk.QueueSubmit(queue, submitInfos, default);
	}

	public static void QueueSubmit(this Vk vk, Queue queue, IEnumerable<SubmitInformation> submitInfos, Fence fence = default) {
		using var infos = submitInfos.GetCreateInfos();
		vk.QueueSubmit(queue, infos.Resources, fence).AssertSuccess();
	}
}