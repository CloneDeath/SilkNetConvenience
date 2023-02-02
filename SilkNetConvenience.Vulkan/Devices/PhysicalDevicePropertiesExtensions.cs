using Silk.NET.Vulkan;

namespace SilkNetConvenience.Devices; 

public static class PhysicalDevicePropertiesExtensions {
	public static unsafe string GetDeviceName(this PhysicalDeviceProperties self) {
		return Helpers.GetString(self.DeviceName);
	}
}