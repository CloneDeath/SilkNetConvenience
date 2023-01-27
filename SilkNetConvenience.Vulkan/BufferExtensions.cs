using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class BufferExtensions {
	public static Buffer CreateBuffer(this Vk vk, Device device, BufferCreateInformation bufferCreateInfo) {
		fixed (uint* queueFamilyIndicesPointer = bufferCreateInfo.QueueFamilyIndices) {
			var createInfo = new BufferCreateInfo {
				SType = StructureType.BufferCreateInfo,
				Flags = bufferCreateInfo.Flags,
				Size = bufferCreateInfo.Size,
				Usage = bufferCreateInfo.Usage,
				SharingMode = bufferCreateInfo.SharingMode,
				PQueueFamilyIndices = queueFamilyIndicesPointer,
				QueueFamilyIndexCount = (uint)bufferCreateInfo.QueueFamilyIndices.Length
			};
			vk.CreateBuffer(device, createInfo, null, out var buffer).AssertSuccess();
			return buffer;
		}
	}
	
	public static void DestroyBuffer(this Vk vk, Device device, Buffer buffer) {
		vk.DestroyBuffer(device, buffer, null);
	}
	
	public static MemoryRequirements GetBufferMemoryRequirements(this Vk vk, Device device, Buffer buffer) {
		vk.GetBufferMemoryRequirements(device, buffer, out var memoryRequirements);
		return memoryRequirements;
	}
}