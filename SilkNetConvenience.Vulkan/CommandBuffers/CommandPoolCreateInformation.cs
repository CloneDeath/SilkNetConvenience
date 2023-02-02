using Silk.NET.Vulkan;

namespace SilkNetConvenience.CommandBuffers;

public class CommandPoolCreateInformation : IGetCreateInfo<CommandPoolCreateInfo> {
	public uint QueueFamilyIndex;
	public CommandPoolCreateFlags Flags;
	
	public ManagedResourceSet<CommandPoolCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<CommandPoolCreateInfo>(new CommandPoolCreateInfo {
			SType = StructureType.CommandPoolCreateInfo,
			Flags = Flags,
			QueueFamilyIndex = QueueFamilyIndex
		}, resources);
	}
}