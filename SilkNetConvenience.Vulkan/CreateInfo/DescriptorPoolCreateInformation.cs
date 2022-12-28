using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo;

public class DescriptorPoolCreateInformation {
	public DescriptorPoolCreateFlags Flags;
	public uint MaxSets;
	public DescriptorPoolSize[] PoolSizes = Array.Empty<DescriptorPoolSize>();
}