using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Descriptors;

public class DescriptorSetAllocateInformation {
	public DescriptorPool DescriptorPool;
	public DescriptorSetLayout[] SetLayouts = Array.Empty<DescriptorSetLayout>();
}