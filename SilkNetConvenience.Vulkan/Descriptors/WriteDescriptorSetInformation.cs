using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Descriptors;

public class WriteDescriptorSetInformation : IGetCreateInfo<WriteDescriptorSet> {
	public DescriptorSet DstSet;
	public uint DstBinding;
	public uint DstArrayElement;
	public uint DescriptorCount;
	public DescriptorType DescriptorType;

	public DescriptorImageInfo[] ImageInfo = Array.Empty<DescriptorImageInfo>();
	public DescriptorBufferInfo[] BufferInfo = Array.Empty<DescriptorBufferInfo>();
	public BufferView[] TexelBufferView = Array.Empty<BufferView>();

	public unsafe ManagedResourceSet<WriteDescriptorSet> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<WriteDescriptorSet>(new WriteDescriptorSet {
			SType = StructureType.WriteDescriptorSet,
			DescriptorCount = DescriptorCount,
			DstBinding = DstBinding,
			DstArrayElement = DstArrayElement,
			DescriptorType = DescriptorType,
			DstSet = DstSet,
			PBufferInfo = resources.AllocateArray(BufferInfo),
			PImageInfo = resources.AllocateArray(ImageInfo),
			PTexelBufferView = resources.AllocateArray(TexelBufferView)
		}, resources);
	}
}