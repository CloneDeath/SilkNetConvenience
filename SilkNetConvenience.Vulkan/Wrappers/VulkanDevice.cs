using System;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.CreateInfo.Descriptors;
using SilkNetConvenience.CreateInfo.Images;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Wrappers; 

public class VulkanDevice : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Instance Instance;
	public readonly PhysicalDevice PhysicalDevice;
	public readonly Device Device;

	public VulkanDevice(VulkanPhysicalDevice physicalDevice, DeviceCreateInformation createInfo) 
		: this(physicalDevice.Vk, physicalDevice.Instance, physicalDevice.PhysicalDevice, createInfo){ }
	public VulkanDevice(Vk vk, Instance instance, PhysicalDevice physicalDevice, DeviceCreateInformation createInfo) {
		Vk = vk;
		Instance = instance;
		PhysicalDevice = physicalDevice;
		Device = vk.CreateDevice(physicalDevice, createInfo);
	}

	protected override void ReleaseVulkanResources() {
		Vk.DestroyDevice(Device);
	}
	
	public KhrSwapchain? GetKhrSwapchainExtension() => Vk.GetKhrSwapchainExtension(Instance, Device);

	public VulkanDeviceMemory AllocateMemoryFor(VulkanImage image, MemoryPropertyFlags flags) {
		var memoryRequirements = image.GetMemoryRequirements();
		return AllocateMemory(memoryRequirements, flags);
	}
	public VulkanDeviceMemory AllocateMemoryFor(VulkanBuffer buffer, MemoryPropertyFlags flags) {
		var memoryRequirements = buffer.GetMemoryRequirements();
		return AllocateMemory(memoryRequirements, flags);
	}
	public VulkanDeviceMemory AllocateMemory(MemoryRequirements requirements, MemoryPropertyFlags flags) {
		return AllocateMemory(FindMemoryType(requirements.MemoryTypeBits, flags), requirements.Size);
	}

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

	public VulkanCommandPool CreateCommandPool(CommandPoolCreateInformation createInfo) => new(this, createInfo);

	public VulkanDescriptorPool CreateDescriptorPool(DescriptorPoolCreateInformation createInfo) => new(this, createInfo);

	public void UpdateDescriptorSets(params WriteDescriptorSetInfo[] writeInfos)
		=> UpdateDescriptorSets(writeInfos, Array.Empty<CopyDescriptorSetInfo>());

	public void UpdateDescriptorSets(params CopyDescriptorSetInfo[] copyInfos) 
		=> UpdateDescriptorSets(Array.Empty<WriteDescriptorSetInfo>(), copyInfos);

	public void UpdateDescriptorSets(WriteDescriptorSetInfo[] writeInfos, CopyDescriptorSetInfo[] copyInfos) 
		=> Vk.UpdateDescriptorSets(Device, writeInfos, copyInfos);

	public VulkanImage CreateImage(ImageCreateInformation imageInfo) => new(this, imageInfo);

	public VulkanImageView CreateImageView(ImageViewCreateInformation viewInfo) => new(this, viewInfo);

	public VulkanSampler CreateSampler(SamplerCreateInformation createInfo) => new(this, createInfo);

	public VulkanRenderPass CreateRenderPass(RenderPassCreateInformation createInfo) => new(this, createInfo);

	public VulkanFramebuffer CreateFramebuffer(FramebufferCreateInformation createInfo) => new(this, createInfo);

	public VulkanSemaphore CreateSemaphore() => new(this);

	public VulkanFence CreateFence(FenceCreateFlags flags = FenceCreateFlags.None) => new(this, flags);

	public PhysicalDeviceMemoryProperties GetMemoryProperties() => Vk.GetPhysicalDeviceMemoryProperties(PhysicalDevice);

	private uint FindMemoryType(uint typeFilter, MemoryPropertyFlags properties) {
		var memoryProperties = Vk.GetPhysicalDeviceMemoryProperties(PhysicalDevice);

		for (var i = 0; i < memoryProperties.MemoryTypeCount; i++) {
			var memType = memoryProperties.MemoryTypes[i];
			if ((typeFilter & (1 << i)) != 0 && (memType.PropertyFlags & properties) == properties) {
				return (uint)i;
			}
		}
		throw new Exception("Failed to find a suitable memory location");
	}
}