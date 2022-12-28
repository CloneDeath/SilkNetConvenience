using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience; 

public class DeviceQueueCreateInformation {
	public DeviceQueueCreateFlags Flags;
	public uint QueueFamilyIndex;
	public float[] QueuePriorities = Array.Empty<float>();
}