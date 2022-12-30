using System;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;

namespace SilkNetConvenience.Wrappers; 

public class VulkanDevice : IDisposable {
	public readonly Vk Vk;
	public readonly Device Device;
	
	public VulkanDevice(PhysicalDevice physicalDevice, DeviceCreateInformation createInfo, Vk vk) {
		Vk = vk;
		Device = vk.CreateDevice(physicalDevice, createInfo);
	}
	
	#region IDisposable
	private void ReleaseUnmanagedResources() {
		Vk.DestroyDevice(Device);
	}

	public void Dispose() {
		ReleaseUnmanagedResources();
		GC.SuppressFinalize(this);
	}

	~VulkanDevice() {
		ReleaseUnmanagedResources();
	}
	#endregion

	public VulkanDeviceMemory AllocateMemory(uint memoryTypeIndex, ulong size) => new(memoryTypeIndex, size, this);
	public VulkanBuffer CreateBuffer(BufferCreateInformation createInfo) => new(createInfo, this);

	public VulkanShaderModule CreateShaderModule(byte[] code) => new(code, this);
	public VulkanShaderModule CreateShaderModule(ShaderModuleCreateInformation createInfo) => new(createInfo, this);
}