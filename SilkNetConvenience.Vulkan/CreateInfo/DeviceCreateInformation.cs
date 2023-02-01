using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo; 

public class DeviceCreateInformation : IGetCreateInfo<DeviceCreateInfo> {
	public string[] EnabledLayers = Array.Empty<string>();
	public string[] EnabledExtensions = Array.Empty<string>();
	public PhysicalDeviceFeatures EnabledFeatures;
	public DeviceQueueCreateInformation[] QueueCreateInfos = Array.Empty<DeviceQueueCreateInformation>();

	public unsafe ManagedResourceSet<DeviceCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<DeviceCreateInfo>(new DeviceCreateInfo {
			SType = StructureType.DeviceCreateInfo,
			QueueCreateInfoCount = (uint)QueueCreateInfos.Length,
			PQueueCreateInfos = resources.AllocateCreateInfos<DeviceQueueCreateInfo, DeviceQueueCreateInformation>(QueueCreateInfos),
			EnabledExtensionCount = (uint)EnabledExtensions.Length,
			PpEnabledExtensionNames = resources.AllocateStringArray(EnabledExtensions),
			EnabledLayerCount = (uint)EnabledLayers.Length,
			PpEnabledLayerNames = resources.AllocateStringArray(EnabledLayers),
			PEnabledFeatures = resources.AllocateStruct(EnabledFeatures)
		}, resources);
	}
}