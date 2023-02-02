using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Buffers; 

public static unsafe class BufferExtensions {
	public static Buffer CreateBuffer(this Vk vk, Device device, BufferCreateInformation createInfo) {
		using var info = createInfo.GetCreateInfo();
		vk.CreateBuffer(device, info.Resource, null, out var buffer).AssertSuccess();
		return buffer;
	}
	
	public static void DestroyBuffer(this Vk vk, Device device, Buffer buffer) {
		vk.DestroyBuffer(device, buffer, null);
	}
	
	public static MemoryRequirements GetBufferMemoryRequirements(this Vk vk, Device device, Buffer buffer) {
		vk.GetBufferMemoryRequirements(device, buffer, out var memoryRequirements);
		return memoryRequirements;
	}
}