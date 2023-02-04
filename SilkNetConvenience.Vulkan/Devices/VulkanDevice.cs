using System;
using Silk.NET.Vulkan;
using SilkNetConvenience.Barriers;
using SilkNetConvenience.Buffers;
using SilkNetConvenience.CommandBuffers;
using SilkNetConvenience.Descriptors;
using SilkNetConvenience.Exceptions;
using SilkNetConvenience.Images;
using SilkNetConvenience.KHR;
using SilkNetConvenience.Memory;
using SilkNetConvenience.Pipelines;
using SilkNetConvenience.Queues;
using SilkNetConvenience.RenderPasses;
using SilkNetConvenience.ShaderModules;

namespace SilkNetConvenience.Devices; 

public class VulkanDevice : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Instance Instance;
	public readonly PhysicalDevice PhysicalDevice;
	public readonly Device Device;

	public VulkanKhrSwapchain KhrSwapchain { get; }

	public VulkanDevice(VulkanPhysicalDevice physicalDevice, DeviceCreateInformation createInfo)
		: this(physicalDevice.Vk, physicalDevice.Instance, physicalDevice.PhysicalDevice, createInfo) {
		physicalDevice.AddChildResource(this);
	}

	public VulkanDevice(Vk vk, Instance instance, PhysicalDevice physicalDevice, DeviceCreateInformation createInfo) {
		Vk = vk;
		Instance = instance;
		PhysicalDevice = physicalDevice;
		Device = vk.CreateDevice(physicalDevice, createInfo);
		KhrSwapchain = new VulkanKhrSwapchain(this);
	}

	protected override void ReleaseVulkanResources() {
		Vk.DestroyDevice(Device);
	}

	public static implicit operator Device(VulkanDevice self) => self.Device;

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

	public void UpdateDescriptorSets(params WriteDescriptorSetInformation[] writeInfos)
		=> UpdateDescriptorSets(writeInfos, Array.Empty<CopyDescriptorSetInformation>());

	public void UpdateDescriptorSets(params CopyDescriptorSetInformation[] copyInfos) 
		=> UpdateDescriptorSets(Array.Empty<WriteDescriptorSetInformation>(), copyInfos);

	public void UpdateDescriptorSets(WriteDescriptorSetInformation[] writeInfos, CopyDescriptorSetInformation[] copyInfos) 
		=> Vk.UpdateDescriptorSets(Device, writeInfos, copyInfos);

	public VulkanImage CreateImage(ImageCreateInformation imageInfo) => new(this, imageInfo);

	public VulkanImageView CreateImageView(ImageViewCreateInformation viewInfo) => new(this, viewInfo);

	public VulkanSampler CreateSampler(SamplerCreateInformation createInfo) => new(this, createInfo);

	public VulkanRenderPass CreateRenderPass(RenderPassCreateInformation createInfo) => new(this, createInfo);

	public VulkanFramebuffer CreateFramebuffer(FramebufferCreateInformation createInfo) => new(this, createInfo);

	public VulkanSemaphore CreateSemaphore() => new(this);

	public VulkanFence CreateFence(FenceCreateFlags flags = FenceCreateFlags.None) => new(this, flags);

	public PhysicalDeviceMemoryProperties GetMemoryProperties() => Vk.GetPhysicalDeviceMemoryProperties(PhysicalDevice);

	// From Vulkan Spec - https://registry.khronos.org/vulkan/specs/1.0/html/vkspec.html#memory-device
	public uint FindMemoryType(uint typeFilter, MemoryPropertyFlags properties) {
		var memoryProperties = Vk.GetPhysicalDeviceMemoryProperties(PhysicalDevice);

		for (var memoryIndex = 0; memoryIndex < memoryProperties.MemoryTypeCount; memoryIndex++) {
			var memType = memoryProperties.MemoryTypes[memoryIndex];
			var isRequiredMemoryType = (typeFilter & (1 << memoryIndex)) != 0;
			var hasRequiredProperties = (memType.PropertyFlags & properties) == properties;
			if (isRequiredMemoryType && hasRequiredProperties) {
				return (uint)memoryIndex;
			}
		}
		throw new Exception("Failed to find a suitable memory location");
	}

	public VulkanPipelineLayout CreatePipelineLayout(params DescriptorSetLayout[] layouts)
		=> CreatePipelineLayout(new PipelineLayoutCreateInformation { SetLayouts = layouts });
	public VulkanPipelineLayout CreatePipelineLayout(PipelineLayoutCreateInformation createInfo) => new(this, createInfo);

	public VulkanPipeline CreateGraphicsPipeline(GraphicsPipelineCreateInformation createInfo) => new(this, createInfo);
}