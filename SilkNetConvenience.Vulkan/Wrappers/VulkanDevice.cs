using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Wrappers; 

public class VulkanDevice : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Instance Instance;
	public readonly Device Device;

	public VulkanDevice(VulkanPhysicalDevice physicalDevice, DeviceCreateInformation createInfo) 
		: this(physicalDevice.Vk, physicalDevice.Instance, physicalDevice.PhysicalDevice, createInfo){ }
	public VulkanDevice(Vk vk, Instance instance, PhysicalDevice physicalDevice, DeviceCreateInformation createInfo) {
		Vk = vk;
		Instance = instance;
		Device = vk.CreateDevice(physicalDevice, createInfo);
	}

	protected override void ReleaseVulkanResources() {
		Vk.DestroyDevice(Device);
	}
	
	public KhrSwapchain? GetKhrSwapchainExtension() => Vk.GetKhrSwapchainExtension(Instance, Device);

	public VulkanDeviceMemory AllocateMemory(MemoryAllocateInformation allocInfo) => new(this, allocInfo);
	public VulkanDeviceMemory AllocateMemory(uint memoryTypeIndex, ulong size) => new(this, memoryTypeIndex, size);
	public VulkanBuffer CreateBuffer(BufferCreateInformation createInfo) => new(this, createInfo);

	public VulkanShaderModule CreateShaderModule(byte[] code) => new(this, code);
	public VulkanShaderModule CreateShaderModule(ShaderModuleCreateInformation createInfo) => new(this, createInfo);

	public VulkanQueue GetDeviceQueue(uint queueFamilyIndex, uint queueIndex) {
		return new VulkanQueue(this, queueFamilyIndex, queueIndex);
	}

	public void WaitIdle() => Vk.DeviceWaitIdle(Device).AssertSuccess();

	public VulkanDescriptorSetLayout CreateDescriptorSetLayout(DescriptorSetLayoutCreateInformation createInfo) => new(this, createInfo);

	public VulkanCommandPool CreateCommandPool(CommandPoolCreateInformation createInfo) => new VulkanCommandPool(this, createInfo);
}