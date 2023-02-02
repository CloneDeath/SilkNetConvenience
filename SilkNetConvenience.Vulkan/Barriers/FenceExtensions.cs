using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Barriers; 

public static unsafe class FenceExtensions {
	public static Fence CreateFence(this Vk vk, Device device, FenceCreateFlags flags = FenceCreateFlags.None) {
		var createInfo = new FenceCreateInfo {
			SType = StructureType.FenceCreateInfo,
			Flags = flags
		};
		vk.CreateFence(device, createInfo, null, out var fence).AssertSuccess();
		return fence;
	}

	public static void DestroyFence(this Vk vk, Device device, Fence fence) {
		vk.DestroyFence(device, fence, null);
	}
}