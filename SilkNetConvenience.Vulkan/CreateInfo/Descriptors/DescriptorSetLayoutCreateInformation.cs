using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Descriptors;

public class DescriptorSetLayoutCreateInformation {
	public DescriptorSetLayoutCreateFlags Flags;
	public DescriptorSetLayoutBindingInformation[] Bindings = Array.Empty<DescriptorSetLayoutBindingInformation>();
}