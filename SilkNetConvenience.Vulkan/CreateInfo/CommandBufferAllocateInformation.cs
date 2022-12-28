using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo;

public class CommandBufferAllocateInformation {
	public CommandPool CommandPool;
	public CommandBufferLevel Level;
	public uint CommandBufferCount;
}