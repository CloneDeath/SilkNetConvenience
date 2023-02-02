using Silk.NET.Vulkan;

namespace SilkNetConvenience.CommandBuffers; 

public class CommandBufferBeginInformation : IGetCreateInfo<CommandBufferBeginInfo> {
	public CommandBufferUsageFlags Flags;
	
	public ManagedResourceSet<CommandBufferBeginInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<CommandBufferBeginInfo>(new CommandBufferBeginInfo {
			SType = StructureType.CommandBufferBeginInfo,
			Flags = Flags
		}, resources);
	}
}