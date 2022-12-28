using System;
using System.Linq;
using System.Runtime.InteropServices;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class VulkanExtensions {
	public static Instance CreateInstance(this Vk self, InstanceCreateInformation info) {
		var appInfo = new ApplicationInfo {
			SType = StructureType.ApplicationInfo,
			ApiVersion = info.ApplicationInfo.ApiVersion,
			ApplicationVersion = info.ApplicationInfo.ApplicationVersion,
			EngineVersion = info.ApplicationInfo.EngineVersion,
			PApplicationName = (byte*)Marshal.StringToHGlobalUni(info.ApplicationInfo.ApplicationName),
			PEngineName = (byte*)Marshal.StringToHGlobalUni(info.ApplicationInfo.EngineName)
		};
		var instanceCreateInfo = new InstanceCreateInfo {
			SType = StructureType.InstanceCreateInfo,
			PApplicationInfo = &appInfo,
			EnabledExtensionCount = (uint)info.EnabledExtensions.Length,
			PpEnabledExtensionNames = (byte**)SilkMarshal.StringArrayToPtr(info.EnabledExtensions),
			EnabledLayerCount = (uint)info.EnabledLayerNames.Length,
			PpEnabledLayerNames = (byte**)SilkMarshal.StringArrayToPtr(info.EnabledLayerNames)
		};

		try {
			self.CreateInstance(instanceCreateInfo, null, out var instance).AssertSuccess();
			return instance;
		}
		finally {
			Marshal.FreeHGlobal((IntPtr)appInfo.PApplicationName);
			Marshal.FreeHGlobal((IntPtr)appInfo.PEngineName);
			if (info.EnabledExtensions.Any()) {
				SilkMarshal.Free((nint)instanceCreateInfo.PpEnabledExtensionNames);
			}

			if (info.EnabledLayerNames.Any()) {
				SilkMarshal.Free((nint)instanceCreateInfo.PpEnabledLayerNames);
			}
		}
	}

	public static LayerProperties[] EnumerateInstanceLayerProperties(this Vk self) {
		return Helpers.GetArray((ref uint length, LayerProperties* data) => self.EnumerateInstanceLayerProperties(ref length, data));
	}

	public static PhysicalDevice[] EnumeratePhysicalDevices(this Vk self, Instance instance) {
		return Helpers.GetArray((ref uint len, PhysicalDevice* data) => self.EnumeratePhysicalDevices(instance, ref len, data));
	}

	public static QueueFamilyProperties[] GetPhysicalDeviceQueueFamilyProperties(this Vk vk, PhysicalDevice physicalDevice) {
		return Helpers.GetArray((ref uint len, QueueFamilyProperties* data) => vk.GetPhysicalDeviceQueueFamilyProperties(physicalDevice, ref len, data));
	}

	public static Device CreateDevice(this Vk vk, PhysicalDevice physicalDevice, DeviceCreateInformation info) {
		var enabledFeatures = info.EnabledFeatures;
		var queueCreateInfos = info.QueueCreateInfos.Select(q => {
			fixed (float* queuePrioritiesPointer = q.QueuePriorities) {
				return new DeviceQueueCreateInfo {
					SType = StructureType.DeviceQueueCreateInfo,
					Flags = q.Flags,
					QueueFamilyIndex = q.QueueFamilyIndex,
					QueueCount = (uint)q.QueuePriorities.Length,
					PQueuePriorities = queuePrioritiesPointer
				};
			}
		}).ToArray();
		fixed (DeviceQueueCreateInfo* queueCreateInfoPointer = queueCreateInfos) {
			var deviceCreateInfo = new DeviceCreateInfo {
				SType = StructureType.DeviceCreateInfo,
				EnabledLayerCount = (uint)info.EnabledLayerNames.Length,
				PpEnabledLayerNames = (byte**)SilkMarshal.StringArrayToPtr(info.EnabledLayerNames),
				EnabledExtensionCount = (uint)info.EnabledExtensionNames.Length,
				PpEnabledExtensionNames = (byte**)SilkMarshal.StringArrayToPtr(info.EnabledExtensionNames),
				PEnabledFeatures = &enabledFeatures,
				QueueCreateInfoCount = (uint)queueCreateInfos.Length,
				PQueueCreateInfos = queueCreateInfoPointer
			};
			try {
				vk.CreateDevice(physicalDevice, deviceCreateInfo, null, out var device).AssertSuccess();
				return device;
			}
			finally {
				SilkMarshal.Free((nint)deviceCreateInfo.PpEnabledLayerNames);
				SilkMarshal.Free((nint)deviceCreateInfo.PpEnabledExtensionNames);
			}
		}
	}

	public static Queue GetDeviceQueue(this Vk vk, Device device, uint queueFamilyIndex, uint queueIndex) {
		vk.GetDeviceQueue(device, queueFamilyIndex, queueIndex, out var queue);
		return queue;
	}

	public static PhysicalDeviceMemoryProperties GetPhysicalDeviceMemoryProperties(this Vk vk, PhysicalDevice physicalDevice) {
		vk.GetPhysicalDeviceMemoryProperties(physicalDevice, out var memoryProperties);
		return memoryProperties;
	}

	public static DeviceMemory AllocateMemory(this Vk vk, Device device, MemoryAllocateInformation allocInfo) {
		var memoryAllocateInfo = new MemoryAllocateInfo {
			SType = StructureType.MemoryAllocateInfo,
			AllocationSize = allocInfo.AllocationSize,
			MemoryTypeIndex = allocInfo.MemoryTypeIndex
		};
		vk.AllocateMemory(device, memoryAllocateInfo, null, out var deviceMemory).AssertSuccess();
		return deviceMemory;
	}

	public static Silk.NET.Vulkan.Buffer CreateBuffer(this Vk vk, Device device, BufferCreateInformation bufferCreateInfo) {
		var createInfo = new BufferCreateInfo {
			SType = StructureType.BufferCreateInfo,
			Flags = bufferCreateInfo.Flags,
			Size = bufferCreateInfo.Size,
			Usage = bufferCreateInfo.Usage,
			SharingMode = bufferCreateInfo.SharingMode,
			
		};
		vk.CreateBuffer(device, createInfo, null, out var buffer).AssertSuccess();
		return buffer;
	}
}