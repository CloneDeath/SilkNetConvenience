using Silk.NET.Vulkan;

namespace SilkNetConvenience.Descriptors;

public class CopyDescriptorSetInformation : IGetCreateInfo<CopyDescriptorSet> {
	public DescriptorSet SrcSet;
	public uint SrcBinding;
	public uint SrcArrayElement;
	public DescriptorSet DstSet;
	public uint DstBinding;
	public uint DstArrayElement;
	public uint DescriptorCount;

	public ManagedResourceSet<CopyDescriptorSet> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<CopyDescriptorSet>(new CopyDescriptorSet {
			SType = StructureType.CopyDescriptorSet,
			DescriptorCount = DescriptorCount,
			DstBinding = DstBinding,
			DstSet = DstSet,
			SrcBinding = SrcBinding,
			SrcSet = SrcSet,
			DstArrayElement = DstArrayElement,
			SrcArrayElement = SrcArrayElement
		}, resources);
	}
}