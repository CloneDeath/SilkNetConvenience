using System;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;

namespace SilkNetConvenience.Wrappers;

public class VulkanDeviceMemory : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly DeviceMemory DeviceMemory;

	public uint MemoryTypeIndex { get; }
	public ulong Size { get; }

	public VulkanDeviceMemory(VulkanDevice device, MemoryAllocateInformation allocInfo)
		: this(device.Vk, device.Device, allocInfo) {
		device.AddChildResource(this);
	}

	public VulkanDeviceMemory(VulkanDevice device, uint memoryTypeIndex, ulong size)
		: this(device.Vk, device.Device, memoryTypeIndex, size) {
		device.AddChildResource(this);
	}
	public VulkanDeviceMemory(Vk vk, Device device, uint memoryTypeIndex, ulong size) 
		: this(vk, device, new MemoryAllocateInformation{ MemoryTypeIndex = memoryTypeIndex, AllocationSize = size}) {}
	public VulkanDeviceMemory(Vk vk, Device device, MemoryAllocateInformation allocInfo)
	{
		Vk = vk;
		Device = device;
		MemoryTypeIndex = allocInfo.MemoryTypeIndex;
		Size = allocInfo.AllocationSize;
		DeviceMemory = vk.AllocateMemory(device, allocInfo);
	}

	protected override void ReleaseVulkanResources() {
		Vk.FreeMemory(Device, DeviceMemory);
	}
	
	public static implicit operator DeviceMemory(VulkanDeviceMemory self) => self.DeviceMemory;

	public Span<T> MapMemory<T>(ulong? offset = null, ulong? size = null) where T : unmanaged {
		var offsetUsed = offset ?? 0;
		var sizeUsed = size ?? (Size - offsetUsed);
		return Vk.MapMemory<T>(Device, DeviceMemory, offsetUsed, sizeUsed);
	}
	
	public Span<byte> MapMemory(ulong? offset = null, ulong? size = null) {
		var offsetUsed = offset ?? 0;
		var sizeUsed = size ?? (Size - offsetUsed);
		return Vk.MapMemory(Device, DeviceMemory, offsetUsed, sizeUsed);
	}
	
	public void UnmapMemory() {
		Vk.UnmapMemory(Device, DeviceMemory);
	}
}