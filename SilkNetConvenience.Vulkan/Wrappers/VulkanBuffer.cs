using System;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using Buffer = Silk.NET.Vulkan.Buffer;

namespace SilkNetConvenience.Wrappers; 

public class VulkanBuffer : IDisposable {
	private readonly Vk _vk;
	private readonly Device _device;
	public readonly Buffer Buffer;

	public VulkanBuffer(BufferCreateInformation createInfo, Device device, Vk vk) {
		_vk = vk;
		_device = device;
		Buffer = vk.CreateBuffer(device, createInfo);
	}

	#region IDisposable
	~VulkanBuffer() {
		FreeUnmanagedResources();
	}

	public void Dispose() {
		FreeUnmanagedResources();
		GC.SuppressFinalize(this);
	}

	private void FreeUnmanagedResources() {
		_vk.DestroyBuffer(_device, Buffer);
	}
	#endregion
	
	public void BindMemory(VulkanDeviceMemory deviceMemory, ulong memoryOffset = 0) => BindMemory(deviceMemory.DeviceMemory, memoryOffset);
	public void BindMemory(DeviceMemory deviceMemory, ulong memoryOffset = 0) => _vk.BindBufferMemory(_device, Buffer, deviceMemory, memoryOffset);
}