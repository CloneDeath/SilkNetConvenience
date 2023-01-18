using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Wrappers; 

public class VulkanDevice : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;

	public VulkanDevice(VulkanContext vk, PhysicalDevice physicalDevice, DeviceCreateInformation createInfo) 
		: this(vk.Vk, physicalDevice, createInfo){ }
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

	public VulkanQueue GetDeviceQueue(uint queueFamilyIndex, uint queueIndex) {
		return new VulkanQueue(this, queueFamilyIndex, queueIndex);
	}

	public void WaitIdle() => Vk.DeviceWaitIdle(Device).AssertSuccess();
}