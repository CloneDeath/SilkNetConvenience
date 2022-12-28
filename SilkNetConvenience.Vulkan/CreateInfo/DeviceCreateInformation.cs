using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo; 

public class DeviceCreateInformation {
	public string[] EnabledLayerNames = Array.Empty<string>();
	public string[] EnabledExtensionNames = Array.Empty<string>();
	public PhysicalDeviceFeatures EnabledFeatures;
	public DeviceQueueCreateInformation[] QueueCreateInfos = Array.Empty<DeviceQueueCreateInformation>();
}