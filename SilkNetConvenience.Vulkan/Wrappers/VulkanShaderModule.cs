using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;

namespace SilkNetConvenience.Wrappers;

public class VulkanShaderModule : BaseVulkanWrapper {
	private readonly Vk _vk;
	private readonly Device _device;
	public readonly ShaderModule ShaderModule;

	public VulkanShaderModule(VulkanDevice device, byte[] code) : this(device.Vk, device.Device, code) {
		device.AddChildResource(this);
	}
	public VulkanShaderModule(Vk vk, Device device, byte[] code) : this(vk, device, new ShaderModuleCreateInformation{ Code = code}){}

	public VulkanShaderModule(VulkanDevice device, ShaderModuleCreateInformation createInfo) : this(device.Vk,
		device.Device, createInfo) {
		device.AddChildResource(this);
	}
	public VulkanShaderModule(Vk vk, Device device, ShaderModuleCreateInformation createInfo) {
		_vk = vk;
		_device = device;
		ShaderModule = vk.CreateShaderModule(_device, createInfo);
	}

	protected override void ReleaseVulkanResources() {
		_vk.DestroyShaderModule(_device, ShaderModule);
	}
	
	public static implicit operator ShaderModule(VulkanShaderModule self) => self.ShaderModule;
}