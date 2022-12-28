using Silk.NET.Vulkan;

namespace SilkNetConvenience;

public class BufferCreateInformation {
	public BufferCreateFlags Flags;
	public ulong Size;
	public BufferUsageFlags Usage;
	public SharingMode SharingMode;
}