using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Buffers;

public class BufferCreateInformation : IGetCreateInfo<BufferCreateInfo> {
	public BufferCreateFlags Flags;
	public ulong Size;
	public BufferUsageFlags Usage;
	public SharingMode SharingMode;
	public uint[] QueueFamilyIndices = Array.Empty<uint>();

	public unsafe ManagedResourceSet<BufferCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<BufferCreateInfo>(new BufferCreateInfo {
			SType = StructureType.BufferCreateInfo,
			Flags = Flags,
			Size = Size,
			Usage = Usage,
			SharingMode = SharingMode,
			QueueFamilyIndexCount = (uint)QueueFamilyIndices.Length,
			PQueueFamilyIndices = resources.AllocateArray(QueueFamilyIndices)
		}, resources);
	}
}