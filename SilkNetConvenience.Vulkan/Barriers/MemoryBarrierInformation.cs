using Silk.NET.Vulkan;

namespace SilkNetConvenience.Barriers; 

public class MemoryBarrierInformation : IGetCreateInfo<MemoryBarrier> {
	public AccessFlags SrcAccessMask;
	public AccessFlags DstAccessMask;

	public ManagedResourceSet<MemoryBarrier> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<MemoryBarrier>(new MemoryBarrier {
			SType = StructureType.MemoryBarrier,
			SrcAccessMask = SrcAccessMask,
			DstAccessMask = DstAccessMask
		}, resources);
	}
}