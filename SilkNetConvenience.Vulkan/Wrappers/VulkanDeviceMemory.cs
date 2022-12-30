using System;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;

namespace SilkNetConvenience.Wrappers;

public class VulkanDeviceMemory : IDisposable {
	private readonly Vk _vk;
	private readonly Device _device;
	public uint MemoryTypeIndex { get; }
	public ulong Size { get; }
	public DeviceMemory DeviceMemory { get; }

	public VulkanDeviceMemory(uint memoryTypeIndex, ulong size, VulkanDevice device) : this(memoryTypeIndex, size, device.Device, device.Vk){}
	public VulkanDeviceMemory(uint memoryTypeIndex, ulong size, Device device, Vk vk) {
		_vk = vk;
		_device = device;
		MemoryTypeIndex = memoryTypeIndex;
		Size = size;
		DeviceMemory = vk.AllocateMemory(device, new MemoryAllocateInformation {
			AllocationSize = size,
			MemoryTypeIndex = memoryTypeIndex
		});
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