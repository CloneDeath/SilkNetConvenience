using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;
using SilkNetConvenience.CreateInfo.KHR;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

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
}