using Silk.NET.Vulkan;

namespace SilkNetConvenience.CommandBuffers;

public class CommandBufferAllocateInformation : IGetCreateInfo<CommandBufferAllocateInfo> {
	public CommandPool CommandPool;
	public CommandBufferLevel Level;
	public uint CommandBufferCount;

	public ManagedResourceSet<CommandBufferAllocateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<CommandBufferAllocateInfo>(new CommandBufferAllocateInfo {
			SType = StructureType.CommandBufferAllocateInfo,
			CommandPool = CommandPool,
			Level = Level,
			CommandBufferCount = CommandBufferCount
		}, resources);
	}
}