using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;

namespace SilkNetConvenience; 

public static unsafe class SwapchainExtensions {
	public static Image[] GetSwapchainImages(this KhrSwapchain khrSwapchain, Device device, SwapchainKHR swapchain) {
		return Helpers.GetArray((ref uint length, Image* data) =>
			khrSwapchain.GetSwapchainImages(device, swapchain, ref length, data));
	}
	public static void DestroySwapchain(this KhrSwapchain khrSwapchain, Device device, SwapchainKHR swapchain) {
		khrSwapchain.DestroySwapchain(device, swapchain, null);
	}
}