using System;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;

namespace SilkNetConvenience.Wrappers;

public class VulkanDeviceMemory : BaseVulkanWrapper {
	private readonly Vk _vk;
	private readonly Device _device;
	public uint MemoryTypeIndex { get; }
	public ulong Size { get; }
	public DeviceMemory DeviceMemory { get; }

	public VulkanDeviceMemory(VulkanDevice device, uint memoryTypeIndex, ulong size) : this(device.Vk, device.Device, memoryTypeIndex, size){}
	public VulkanDeviceMemory(Vk vk, Device device, uint memoryTypeIndex, ulong size) {
		_vk = vk;
		_device = device;
		MemoryTypeIndex = memoryTypeIndex;
		Size = size;
		DeviceMemory = vk.AllocateMemory(device, new MemoryAllocateInformation {
			AllocationSize = size,
			MemoryTypeIndex = memoryTypeIndex
		});
	}

	protected override void ReleaseVulkanResources() {
		_vk.FreeMemory(_device, DeviceMemory);
	}
	
	public Span<byte> MapMemory() {
		return _vk.MapMemory(_device, DeviceMemory, 0, Size);
	}
	
	public void UnmapMemory() {
		_vk.UnmapMemory(_device, DeviceMemory);
	}
}