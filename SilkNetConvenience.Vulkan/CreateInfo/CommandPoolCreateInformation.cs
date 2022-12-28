using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo;

public class CommandPoolCreateInformation {
	public uint QueueFamilyIndex;
	public CommandPoolCreateFlags Flags;
}