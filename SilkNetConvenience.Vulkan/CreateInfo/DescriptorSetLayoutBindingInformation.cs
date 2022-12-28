using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo;

public class DescriptorSetLayoutBindingInformation {
	public uint Binding;
	public DescriptorType DescriptorType;
	public uint DescriptorCount;
	public ShaderStageFlags StageFlags;
	public Sampler[] ImmutableSamplers = Array.Empty<Sampler>();
}