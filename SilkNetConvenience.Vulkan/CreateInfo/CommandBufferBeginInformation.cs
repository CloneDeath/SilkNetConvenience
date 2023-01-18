using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo; 

public class CommandBufferBeginInformation {
	public CommandBufferUsageFlags Flags;
	
	public CommandBufferBeginInfo GetBeginInfo() {
		return new CommandBufferBeginInfo {
			SType = StructureType.CommandBufferBeginInfo,
			Flags = Flags
		};
	}
}