using Silk.NET.Vulkan;

namespace SilkNetConvenience; 

public static class BufferExtensions {
	public static MemoryRequirements GetBufferMemoryRequirements(this Vk vk, Device device, Buffer buffer) {
		vk.GetBufferMemoryRequirements(device, buffer, out var memoryRequirements);
		return memoryRequirements;
	}
}