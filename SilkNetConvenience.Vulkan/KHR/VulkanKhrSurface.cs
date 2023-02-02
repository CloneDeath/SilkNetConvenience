using System;
using Silk.NET.Core.Contexts;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;
using SilkNetConvenience.Instances;

namespace SilkNetConvenience.KHR; 

public class VulkanKhrSurface : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Instance Instance;
	public readonly KhrSurface KhrSurface;

	public VulkanKhrSurface(VulkanInstance instance) : this(instance.Vk, instance.Instance) {
		instance.AddChildResource(this);
	}
	public VulkanKhrSurface(Vk vk, Instance instance) {
		Vk = vk;
		Instance = instance;
		KhrSurface = Vk.GetKhrSurfaceExtension(Instance) ?? throw new NullReferenceException();
	}
	
	protected override void ReleaseVulkanResources() { }
	
	public static implicit operator KhrSurface(VulkanKhrSurface self) => self.KhrSurface;

	public VulkanSurface CreateSurface(IVkSurface vkSurface) => new(this, vkSurface);

	public bool GetPhysicalDeviceSurfaceSupport(PhysicalDevice physicalDevice, uint queueFamilyIndex, SurfaceKHR surface) {
		return KhrSurface.GetPhysicalDeviceSurfaceSupport(physicalDevice, queueFamilyIndex, surface);
	}

	public SurfaceCapabilitiesKHR GetPhysicalDeviceSurfaceCapabilities(PhysicalDevice physicalDevice, SurfaceKHR surface) {
		return KhrSurface.GetPhysicalDeviceSurfaceCapabilities(physicalDevice, surface);
	}

	public SurfaceFormatKHR[] GetPhysicalDeviceSurfaceFormats(PhysicalDevice physicalDevice, SurfaceKHR surface) {
		return KhrSurface.GetPhysicalDeviceSurfaceFormats(physicalDevice, surface);
	}

	public PresentModeKHR[] GetPhysicalDeviceSurfacePresentModes(PhysicalDevice physicalDevice, SurfaceKHR surface) {
		return KhrSurface.GetPhysicalDeviceSurfacePresentModes(physicalDevice, surface);
	}
}