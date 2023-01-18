using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Wrappers; 

public class VulkanCommandBuffer : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly CommandPool CommandPool;
	public readonly CommandBuffer CommandBuffer;

	public VulkanCommandBuffer(VulkanCommandPool commandPool, CommandBufferLevel level)
		: this(commandPool.Vk, commandPool.Device, commandPool.CommandPool, level){}
	public VulkanCommandBuffer(Vk vk, Device device, CommandPool commandPool, CommandBufferLevel level) {
		Vk = vk;
		Device = device;
		CommandPool = commandPool;
		CommandBuffer = vk.AllocateCommandBuffers(device, new CommandBufferAllocateInformation {
			Level = level,
			CommandBufferCount = 1,
			CommandPool = commandPool
		})[0];
	}

	protected override void ReleaseVulkanResources() {
		Vk.FreeCommandBuffers(Device, CommandPool, 1, CommandBuffer);
	}

	public void Begin(CommandBufferUsageFlags usage) {
		Vk.BeginCommandBuffer(CommandBuffer, new CommandBufferBeginInformation {
			Flags = usage
		});
	}

	public void CopyBuffer(VulkanBuffer source, VulkanBuffer destination, ulong size)
		=> CopyBuffer(source, destination, new BufferCopy { Size = size });
	public void CopyBuffer(VulkanBuffer source, VulkanBuffer destination, params BufferCopy[] copies) {
		Vk.CmdCopyBuffer(CommandBuffer, source.Buffer, destination.Buffer, copies);
	}

	public void End() {
		Vk.EndCommandBuffer(CommandBuffer).AssertSuccess();
	}
}