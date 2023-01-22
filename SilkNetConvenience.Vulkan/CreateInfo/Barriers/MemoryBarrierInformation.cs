using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Barriers; 

public class MemoryBarrierInformation {
	public AccessFlags SrcAccessMask;
	public AccessFlags DstAccessMask;

	public MemoryBarrier GetBarrier() {
		return new MemoryBarrier {
			SType = StructureType.MemoryBarrier,
			SrcAccessMask = SrcAccessMask,
			DstAccessMask = DstAccessMask
		};
	}
}