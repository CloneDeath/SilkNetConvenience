using Silk.NET.Core.Contexts;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;

namespace SilkNetConvenience.Wrappers.KHR; 

public class VulkanSurface : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Instance Instance;
	public readonly KhrSurface KhrSurface;
	public readonly SurfaceKHR Surface;

	public VulkanSurface(VulkanKhrSurface khrSurface, IVkSurface surface)
		: this(khrSurface.Vk, khrSurface.Instance, khrSurface.KhrSurface, surface) {
		khrSurface.AddChildResource(this);
	}
	public unsafe VulkanSurface(Vk vk, Instance instance, KhrSurface khrSurface, IVkSurface vkSurface) {
		Vk = vk;
		Instance = instance;
		KhrSurface = khrSurface;
		Surface = vkSurface.Create<AllocationCallbacks>(instance.ToHandle(), null).ToSurface();
	}
	
	protected override void ReleaseVulkanResources() {
		KhrSurface.DestroySurface(Instance, Surface);
	}
	
	public static implicit operator SurfaceKHR(VulkanSurface self) => self.Surface;
}