using System;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class MemoryExtensions {
	public static DeviceMemory AllocateMemory(this Vk vk, Device device, MemoryAllocateInformation allocInfo) {
		var memoryAllocateInfo = new MemoryAllocateInfo {
			SType = StructureType.MemoryAllocateInfo,
			AllocationSize = allocInfo.AllocationSize,
			MemoryTypeIndex = allocInfo.MemoryTypeIndex
		};
		vk.AllocateMemory(device, memoryAllocateInfo, null, out var deviceMemory).AssertSuccess();
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