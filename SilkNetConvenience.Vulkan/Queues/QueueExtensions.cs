using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Queues; 

public static class QueueExtensions {
	public static Queue GetDeviceQueue(this Vk vk, Device device, uint queueFamilyIndex, uint queueIndex) {
		vk.GetDeviceQueue(device, queueFamilyIndex, queueIndex, out var queue);
		return queue;
	}
	
	public static void QueueSubmit(this Vk vk, Queue queue, SubmitInformation[] submitInfos, Fence fence) {
		using var infos = submitInfos.GetCreateInfos();
		vk.QueueSubmit(queue, infos.Resources, fence).AssertSuccess();
	}
}