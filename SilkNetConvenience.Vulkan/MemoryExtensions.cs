using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience; 

public static unsafe class MemoryExtensions {
	public static Span<byte> MapMemory(this Vk vk, Device device, DeviceMemory memory, ulong offset, ulong size) {
		void* data;
		vk.MapMemory(device, memory, offset, size, 0, &data);
		return new Span<byte>(data, (int)size);
	}
	
	public static Span<T> MapMemory<T>(this Vk vk, Device device, DeviceMemory memory, ulong offset, ulong size)
										where T : unmanaged {
		void* data;
		vk.MapMemory(device, memory, offset, size, 0, &data);
		return new Span<T>(data, (int)((long)size / sizeof(T)));
	}
}