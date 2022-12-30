using System;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;

namespace SilkNetConvenience.Wrappers;

public class VulkanDeviceMemory : IDisposable {
	private readonly Vk _vk;
	private readonly Device _device;
	public DeviceMemory DeviceMemory { get; }
	public uint MemoryTypeIndex { get; }
	public ulong Size { get; }

	public VulkanDeviceMemory(uint memoryTypeIndex, ulong size, Device device, Vk vk) {
		_vk = vk;
		_device = device;
		DeviceMemory = vk.AllocateMemory(device, new MemoryAllocateInformation {
			AllocationSize = size,
			MemoryTypeIndex = memoryTypeIndex
		});
		MemoryTypeIndex = memoryTypeIndex;
		Size = size;
	}

	#region IDisposable
	~VulkanDeviceMemory() {
		FreeUnmanagedResources();
	}

	public void Dispose() {
		FreeUnmanagedResources();
		GC.SuppressFinalize(this);
	}

	private void FreeUnmanagedResources() {
		_vk.FreeMemory(_device, DeviceMemory);
	}
	#endregion

	public Span<byte> MapMemory() {
		return _vk.MapMemory(_device, DeviceMemory, 0, Size);
	}
	
	public void UnmapMemory() {
		_vk.UnmapMemory(_device, DeviceMemory);
	}
}