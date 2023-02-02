using Silk.NET.Vulkan;

namespace SilkNetConvenience.Memory; 

public class MemoryAllocateInformation : IGetCreateInfo<MemoryAllocateInfo> {
	public ulong AllocationSize;
	public uint MemoryTypeIndex;

	public ManagedResourceSet<MemoryAllocateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<MemoryAllocateInfo>(new MemoryAllocateInfo {
			SType = StructureType.MemoryAllocateInfo,
			AllocationSize = AllocationSize,
			MemoryTypeIndex = MemoryTypeIndex
		}, resources);
	}
}