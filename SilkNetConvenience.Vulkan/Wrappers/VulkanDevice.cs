using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;

namespace SilkNetConvenience.Wrappers; 

public class VulkanDevice : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	
	public VulkanDevice(Vk vk, PhysicalDevice physicalDevice, DeviceCreateInformation createInfo) {
		Vk = vk;
		Device = vk.CreateDevice(physicalDevice, createInfo);
	}

	protected override void ReleaseVulkanResources() {
		Vk.DestroyDevice(Device);
	}

	public VulkanDeviceMemory AllocateMemory(uint memoryTypeIndex, ulong size) => new(this, memoryTypeIndex, size);
	public VulkanBuffer CreateBuffer(BufferCreateInformation createInfo) => new(this, createInfo);

	public VulkanShaderModule CreateShaderModule(byte[] code) => new(this, code);
	public VulkanShaderModule CreateShaderModule(ShaderModuleCreateInformation createInfo) => new(this, createInfo);
}