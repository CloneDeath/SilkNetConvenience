using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Descriptors;

public class WriteDescriptorSetInfo {
	public DescriptorSet DstSet;
	public uint DstBinding;
	public uint DstArrayElement;
	public uint DescriptorCount;
	public DescriptorType DescriptorType;

	public DescriptorImageInfo[] ImageInfo = Array.Empty<DescriptorImageInfo>();
	public DescriptorBufferInfo[] BufferInfo = Array.Empty<DescriptorBufferInfo>();
	public BufferView[] TexelBufferView = Array.Empty<BufferView>();
}