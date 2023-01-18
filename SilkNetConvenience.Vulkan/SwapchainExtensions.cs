using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;

namespace SilkNetConvenience; 

public static unsafe class SwapchainExtensions {
	public static void DestroySwapchain(this KhrSwapchain khrSwapchain, Device device, SwapchainKHR swapchain) {
		khrSwapchain.DestroySwapchain(device, swapchain, null);
	}
}