using System;
using System.Runtime.InteropServices;
using Silk.NET.Vulkan;

namespace SilkNetConvenience; 

public static unsafe class ExtensionPropertiesExtensions {
	public static string GetExtensionName(this ExtensionProperties self) {
		return Marshal.PtrToStringAnsi((IntPtr)self.ExtensionName) ?? string.Empty;
	}
}