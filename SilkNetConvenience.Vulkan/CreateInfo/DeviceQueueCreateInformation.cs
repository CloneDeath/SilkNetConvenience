using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo; 

public class DeviceQueueCreateInformation {
	public DeviceQueueCreateFlags Flags;
	public uint QueueFamilyIndex;
	public float[] QueuePriorities = Array.Empty<float>();
}