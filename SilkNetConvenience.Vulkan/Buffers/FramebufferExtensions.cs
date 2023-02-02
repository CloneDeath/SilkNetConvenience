using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Buffers; 

public static unsafe class FramebufferExtensions {
	public static Framebuffer CreateFramebuffer(this Vk vk, Device device, FramebufferCreateInformation createInfo) {
		using var info = createInfo.GetCreateInfo();
		vk.CreateFramebuffer(device, info.Resource, null, out var framebuffer).AssertSuccess();
		return framebuffer;
	}
	
	public static void DestroyFramebuffer(this Vk vk, Device device, Framebuffer framebuffer) {
		vk.DestroyFramebuffer(device, framebuffer, null);
	}
}