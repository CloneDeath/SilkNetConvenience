using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.KHR; 

public static unsafe class SurfaceExtensions {
	public static void DestroySurface(this KhrSurface khrSurface, Instance instance, SurfaceKHR surface) {
		khrSurface.DestroySurface(instance, surface, null);
	}
	
	public static bool GetPhysicalDeviceSurfaceSupport(this KhrSurface khrSurface, PhysicalDevice physicalDevice, 
													   uint queueFamilyIndex, SurfaceKHR surface) {
		khrSurface.GetPhysicalDeviceSurfaceSupport(physicalDevice, queueFamilyIndex, surface, out var supported).AssertSuccess();
		return supported;
	}

	public static SurfaceCapabilitiesKHR GetPhysicalDeviceSurfaceCapabilities(this KhrSurface khrSurface,
																			  PhysicalDevice physicalDevice,
																			  SurfaceKHR surface) {
		khrSurface.GetPhysicalDeviceSurfaceCapabilities(physicalDevice, surface, out var capabilities).AssertSuccess();
		return capabilities;
	}
	
	public static SurfaceFormatKHR[] GetPhysicalDeviceSurfaceFormats(this KhrSurface khrSurface,
																   PhysicalDevice physicalDevice,
																   SurfaceKHR surface) {
		return Helpers.GetArray((ref uint len, SurfaceFormatKHR* data) 
								=> khrSurface.GetPhysicalDeviceSurfaceFormats(physicalDevice, surface, ref len, data));
	}
	
	public static PresentModeKHR[] GetPhysicalDeviceSurfacePresentModes(this KhrSurface khrSurface,
																		PhysicalDevice physicalDevice,
																		SurfaceKHR surface) {
		return Helpers.GetArray((ref uint len, PresentModeKHR* data) 
								=> khrSurface.GetPhysicalDeviceSurfacePresentModes(physicalDevice, surface, ref len, data));
	}
}