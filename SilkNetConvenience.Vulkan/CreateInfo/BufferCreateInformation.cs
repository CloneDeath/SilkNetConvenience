using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo;

public class BufferCreateInformation {
	public BufferCreateFlags Flags;
	public ulong Size;
	public BufferUsageFlags Usage;
	public SharingMode SharingMode;
	public uint[] QueueFamilyIndices = Array.Empty<uint>();
}