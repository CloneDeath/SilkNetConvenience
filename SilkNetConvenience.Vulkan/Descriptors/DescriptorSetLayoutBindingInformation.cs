using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Descriptors;

public class DescriptorSetLayoutBindingInformation : IGetCreateInfo<DescriptorSetLayoutBinding> {
	public uint Binding;
	public DescriptorType DescriptorType;
	public uint DescriptorCount;
	public ShaderStageFlags StageFlags;
	public Sampler[] ImmutableSamplers = Array.Empty<Sampler>();

	public unsafe ManagedResourceSet<DescriptorSetLayoutBinding> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<DescriptorSetLayoutBinding>(new DescriptorSetLayoutBinding {
			DescriptorType = DescriptorType,
			DescriptorCount = DescriptorCount,
			Binding = Binding,
			StageFlags = StageFlags,
			PImmutableSamplers = resources.AllocateArray(ImmutableSamplers)
		}, resources);
	}
}