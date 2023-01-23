using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Descriptors;

public class CopyDescriptorSetInfo {
	public DescriptorSet SrcSet;
	public uint SrcBinding;
	public uint SrcArrayElement;
	public DescriptorSet DstSet;
	public uint DstBinding;
	public uint DstArrayElement;
	public uint DescriptorCount;
}