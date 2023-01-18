using Silk.NET.Vulkan;

namespace SilkNetConvenience; 

public static unsafe class FramebufferExtensions {
	public static void DestroyFramebuffer(this Vk vk, Device device, Framebuffer framebuffer) {
		vk.DestroyFramebuffer(device, framebuffer, null);
	}
}