using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Devices; 

public class DeviceQueueCreateInformation : IGetCreateInfo<DeviceQueueCreateInfo> {
	public DeviceQueueCreateFlags Flags;
	public uint QueueFamilyIndex;
	public float[] QueuePriorities = Array.Empty<float>();

	public unsafe ManagedResourceSet<DeviceQueueCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<DeviceQueueCreateInfo>(new DeviceQueueCreateInfo {
			SType = StructureType.DeviceQueueCreateInfo,
			Flags = Flags,
			QueueFamilyIndex = QueueFamilyIndex,
			QueueCount = (uint)QueuePriorities.Length,
			PQueuePriorities = resources.AllocateArray(QueuePriorities)
		}, resources);
	}
}