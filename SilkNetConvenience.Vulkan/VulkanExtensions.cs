using System;
using System.Linq;
using System.Runtime.InteropServices;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
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
}