using Silk.NET.Vulkan;

namespace SilkNetConvenience.Barriers; 

public class BufferMemoryBarrierInformation : IGetCreateInfo<BufferMemoryBarrier> {
	public Buffer Buffer;
	public AccessFlags DstAccessMask;
	public ulong Offset;
	public ulong Size;
	public AccessFlags SrcAccessMask;
	public uint DstQueueFamilyIndex;
	public uint SrcQueueFamilyIndex;

	public ManagedResourceSet<BufferMemoryBarrier> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<BufferMemoryBarrier>(new BufferMemoryBarrier {
			SType = StructureType.BufferMemoryBarrier,
			Buffer = Buffer,
			DstAccessMask = DstAccessMask,
			Offset = Offset,
			Size = Size,
			SrcAccessMask = SrcAccessMask,
			DstQueueFamilyIndex = DstQueueFamilyIndex,
			SrcQueueFamilyIndex = SrcQueueFamilyIndex
		}, resources);
	}
}