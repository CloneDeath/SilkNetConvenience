using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo;

public class DescriptorSetAllocateInformation {
	public DescriptorPool DescriptorPool;
	public DescriptorSetLayout[] SetLayouts = Array.Empty<DescriptorSetLayout>();
}