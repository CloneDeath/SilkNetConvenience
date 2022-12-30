using System;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;

namespace SilkNetConvenience.Wrappers;

public class VulkanMemory : IDisposable {
	public DeviceMemory Memory { get; }
	public uint MemoryTypeIndex { get; }
	public ulong Size { get; }
	private readonly Device _device;
	private readonly Vk _vk;

	public VulkanMemory(uint memoryTypeIndex, ulong size, Device device, Vk vk) {
		Memory = vk.AllocateMemory(device, new MemoryAllocateInformation {
			AllocationSize = size,
			MemoryTypeIndex = memoryTypeIndex
		});
		MemoryTypeIndex = memoryTypeIndex;
		Size = size;
		_device = device;
		_vk = vk;
	}

	#region IDisposable
	~VulkanMemory() {
		FreeUnmanagedResources();
	}

	public void Dispose() {
		FreeUnmanagedResources();
		GC.SuppressFinalize(this);
	}

	private void FreeUnmanagedResources() {
		_vk.FreeMemory(_device, Memory);
	}
	#endregion

	public Span<byte> MapMemory() {
		return _vk.MapMemory(_device, Memory, 0, Size);
	}
	
	public void UnmapMemory() {
		_vk.UnmapMemory(_device, Memory);
	}
}