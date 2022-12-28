using System;
using System.Collections.Generic;
using Silk.NET.Vulkan;

namespace SilkNetConvenience; 

public class DeviceCreateInformation {
	public string[] EnabledLayerNames = Array.Empty<string>();
	public string[] EnabledExtensionNames = Array.Empty<string>();
	public PhysicalDeviceFeatures EnabledFeatures;
	public DeviceQueueCreateInformation[] QueueCreateInfos = Array.Empty<DeviceQueueCreateInformation>();
}