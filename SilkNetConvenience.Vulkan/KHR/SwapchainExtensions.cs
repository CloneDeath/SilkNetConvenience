using System;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;
using SilkNetConvenience.Exceptions;
using SilkNetConvenience.Exceptions.ResultExceptions;

namespace SilkNetConvenience.KHR; 

public static unsafe class SwapchainExtensions {
	public static SwapchainKHR CreateSwapchain(this KhrSwapchain khrSwapchain, Device device, SwapchainCreateInformation createInfo) {
		using var info = createInfo.GetCreateInfo();
		khrSwapchain.CreateSwapchain(device, info.Resource, null, out var swapchain).AssertSuccess();
		return swapchain;
	}
	
	public static void DestroySwapchain(this KhrSwapchain khrSwapchain, Device device, SwapchainKHR swapchain) {
		khrSwapchain.DestroySwapchain(device, swapchain, null);
	}

	public static Image[] GetSwapchainImages(this KhrSwapchain khrSwapchain, Device device, SwapchainKHR swapchain) {
		return Helpers.GetArray((ref uint length, Image* data) =>
			khrSwapchain.GetSwapchainImages(device, swapchain, ref length, data));
	}
	
	public static uint AcquireNextImage(this KhrSwapchain khrSwapchain, Device device, SwapchainKHR swapchain,
										TimeSpan? timeout, Semaphore semaphore, Fence fence) {
		uint imageIndex = 0;
		try {
			khrSwapchain.AcquireNextImage(device, swapchain, timeout.GetTotalNanoSeconds(), semaphore, fence,
										  ref imageIndex).AssertSuccess();
		}
		catch (SuboptimalKhrException){}
		return imageIndex;
	}
	
	public static void QueuePresent(this KhrSwapchain khrSwapchain, Queue queue, PresentInformation presentInfo) {
		using var info = presentInfo.GetCreateInfo();
		khrSwapchain.QueuePresent(queue, info.Resource).AssertSuccess();
	}
}