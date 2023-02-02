using System;
using System.Linq;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;

namespace SilkNetConvenience.KHR; 

public class VulkanSwapchain : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly KhrSwapchain KhrSwapchain;
	public readonly SwapchainKHR Swapchain;

	public VulkanSwapchain(VulkanKhrSwapchain khrSwapchain, SwapchainCreateInformation createInfo)
		: this(khrSwapchain.Vk, khrSwapchain.Device, khrSwapchain.KhrSwapchain, createInfo) {
		khrSwapchain.AddChildResource(this);
	}
	public VulkanSwapchain(Vk vk, Device device, KhrSwapchain khrSwapchain, SwapchainCreateInformation createInfo) {
		Vk = vk;
		Device = device;
		KhrSwapchain = khrSwapchain;
		Swapchain = KhrSwapchain.CreateSwapchain(Device, createInfo);
	}
	
	protected override void ReleaseVulkanResources() {
		KhrSwapchain.DestroySwapchain(Device, Swapchain);
	}
	
	public static implicit operator SwapchainKHR(VulkanSwapchain self) => self.Swapchain;

	public VulkanSwapchainImage[] GetImages() {
		var images = KhrSwapchain.GetSwapchainImages(Device, Swapchain);
		return images.Select(i => new VulkanSwapchainImage(this, i)).ToArray();
	}

	public uint AcquireNextImage(Semaphore semaphore) => AcquireNextImage(null, semaphore, default);
	public uint AcquireNextImage(TimeSpan? timeout, Semaphore semaphore, Fence fence) {
		return KhrSwapchain.AcquireNextImage(Device, Swapchain, timeout, semaphore, fence);
	}
}