using System;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;

namespace SilkNetConvenience.Wrappers;

public class VulkanShaderModule : IDisposable {
	private readonly Vk _vk;
	private readonly Device _device;
	public readonly ShaderModule ShaderModule;
	
	public VulkanShaderModule(byte[] code, VulkanDevice device) : this(code, device.Device, device.Vk){}
	public VulkanShaderModule(byte[] code, Device device, Vk vk) : this(new ShaderModuleCreateInformation{ Code = code}, device, vk){}
	public VulkanShaderModule(ShaderModuleCreateInformation createInfo, VulkanDevice device) : this(createInfo, device.Device, device.Vk){}
	public VulkanShaderModule(ShaderModuleCreateInformation createInfo, Device device, Vk vk) {
		_vk = vk;
		_device = device;
		ShaderModule = vk.CreateShaderModule(_device, createInfo);
	}

	#region IDisposable
	private void ReleaseUnmanagedResources() {
		_vk.DestroyShaderModule(_device, ShaderModule);
	}

	public void Dispose() {
		ReleaseUnmanagedResources();
		GC.SuppressFinalize(this);
	}

	~VulkanShaderModule() {
		ReleaseUnmanagedResources();
	}
	#endregion
}