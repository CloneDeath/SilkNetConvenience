using System;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;
using SilkNetConvenience.CreateInfo.KHR;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Wrappers.KHR; 

public class VulkanKhrSwapchain : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Instance Instance;
	public readonly Device Device;
	public readonly KhrSwapchain KhrSwapchain;

	public VulkanKhrSwapchain(VulkanDevice device) : this(device.Vk, device.Instance, device.Device) {
		device.AddChildResource(this);
	}
	public VulkanKhrSwapchain(Vk vk, Instance instance, Device device) {
		Vk = vk;
		Instance = instance;
		Device = device;
		KhrSwapchain = Vk.GetKhrSwapchainExtension(Instance, Device) 
					   ?? throw new NotSupportedException("VK_KHR_swapchain extension not found.");
	}

	protected override void ReleaseVulkanResources() { }
	
	public static implicit operator KhrSwapchain(VulkanKhrSwapchain self) => self.KhrSwapchain;

	public VulkanSwapchain CreateSwapchain(SwapchainCreateInformation createInfo) => new(this, createInfo);

	public void QueuePresent(Queue queue, PresentInformation presentInfo) {
		KhrSwapchain.QueuePresent(queue, presentInfo);
	}
}