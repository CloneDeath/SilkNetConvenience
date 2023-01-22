using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Barriers; 

public class BufferMemoryBarrierInformation {
	public Buffer Buffer;
	public AccessFlags DstAccessMask;
	public ulong Offset;
	public ulong Size;
	public AccessFlags SrcAccessMask;
	public uint DstQueueFamilyIndex;
	public uint SrcQueueFamilyIndex;

	public BufferMemoryBarrier GetBarrier() {
		return new BufferMemoryBarrier {
			SType = StructureType.BufferMemoryBarrier,
			Buffer = Buffer,
			DstAccessMask = DstAccessMask,
			Offset = Offset,
			Size = Size,
			SrcAccessMask = SrcAccessMask,
			DstQueueFamilyIndex = DstQueueFamilyIndex,
			SrcQueueFamilyIndex = SrcQueueFamilyIndex
		};
	}
}