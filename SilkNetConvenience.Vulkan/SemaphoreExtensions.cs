using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class SemaphoreExtensions {
	public static Semaphore CreateSemaphore(this Vk vk, Device device) {
		var createInfo = new SemaphoreCreateInfo {
			SType = StructureType.SemaphoreCreateInfo
		};
		vk.CreateSemaphore(device, createInfo, null, out var semaphore).AssertSuccess();
		return semaphore;
	}

	public static void DestroySemaphore(this Vk vk, Device device, Semaphore semaphore) {
		vk.DestroySemaphore(device, semaphore, null);
	}
}