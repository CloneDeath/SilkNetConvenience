using System.Linq;
using Silk.NET.Vulkan;
using SilkNetConvenience.Devices;

namespace SilkNetConvenience.CommandBuffers; 

public class VulkanCommandPool : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly CommandPool CommandPool;

	public VulkanCommandPool(VulkanDevice device, CommandPoolCreateInformation createInfo)
		: this(device.Vk, device.Device, createInfo) {
		device.AddChildResource(this);
	}
	public VulkanCommandPool(Vk vk, Device device, CommandPoolCreateInformation createInfo) {
		Vk = vk;
		Device = device;
		CommandPool = vk.CreateCommandPool(device, createInfo);
	}

	protected override void ReleaseVulkanResources() {
		Vk.DestroyCommandPool(Device, CommandPool);
	}
	
	public static implicit operator CommandPool(VulkanCommandPool self) => self.CommandPool;
	
	public VulkanCommandBuffer AllocateCommandBuffer(CommandBufferLevel level = CommandBufferLevel.Primary) {
		return new VulkanCommandBuffer(this, level);
	}

	public VulkanCommandBuffer[] AllocateCommandBuffers(uint count, CommandBufferLevel level = CommandBufferLevel.Primary) {
		var allocInfo = new CommandBufferAllocateInformation {
			CommandPool = CommandPool,
			CommandBufferCount = count,
			Level = level
		};

		var buffers = Vk.AllocateCommandBuffers(Device, allocInfo);
		return buffers.Select(b => new VulkanCommandBuffer(Vk, Device, CommandPool, b)).ToArray();
	}
}