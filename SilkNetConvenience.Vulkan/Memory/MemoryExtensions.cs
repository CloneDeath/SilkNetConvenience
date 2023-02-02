using System;
using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Memory; 

public static unsafe class MemoryExtensions {
	public static DeviceMemory AllocateMemory(this Vk vk, Device device, MemoryAllocateInformation allocInfo) {
		using var info = allocInfo.GetCreateInfo();
		vk.AllocateMemory(device, info.Resource, null, out var deviceMemory).AssertSuccess();
		return deviceMemory;
	}
	
	public static void FreeMemory(this Vk vk, Device device, DeviceMemory memory) {
		vk.FreeMemory(device, memory, null);
	}
	
	public static Span<byte> MapMemory(this Vk vk, Device device, DeviceMemory memory, ulong offset, ulong size) {
		void* data;
		vk.MapMemory(device, memory, offset, size, 0, &data).AssertSuccess();
		return new Span<byte>(data, (int)size);
	}
	
	public static Span<T> MapMemory<T>(this Vk vk, Device device, DeviceMemory memory, ulong offset, ulong size)
										where T : unmanaged {
		void* data;
		vk.MapMemory(device, memory, offset, size, 0, &data).AssertSuccess();
		return new Span<T>(data, (int)((long)size / sizeof(T)));
	}
}