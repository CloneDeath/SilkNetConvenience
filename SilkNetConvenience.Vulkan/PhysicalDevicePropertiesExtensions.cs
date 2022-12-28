using Silk.NET.Vulkan;

namespace SilkNetConvenience; 

public static class PhysicalDevicePropertiesExtensions {
	public static unsafe string GetDeviceName(this PhysicalDeviceProperties self) {
		return Helpers.GetString(self.DeviceName);
	}
}