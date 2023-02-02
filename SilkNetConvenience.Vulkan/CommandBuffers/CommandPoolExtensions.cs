using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.CommandBuffers; 

public static unsafe class CommandPoolExtensions {
	public static CommandPool CreateCommandPool(this Vk vk, Device device, CommandPoolCreateInformation createInfo) {
		using var info = createInfo.GetCreateInfo();
		vk.CreateCommandPool(device, info.Resource, null, out var commandPool).AssertSuccess();
		return commandPool;
	}

	public static void DestroyCommandPool(this Vk vk, Device device, CommandPool commandPool) {
		vk.DestroyCommandPool(device, commandPool, null);
	}
}