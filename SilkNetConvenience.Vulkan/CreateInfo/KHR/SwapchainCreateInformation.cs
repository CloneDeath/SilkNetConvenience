using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.KHR; 

public class SwapchainCreateInformation : IGetCreateInfo<SwapchainCreateInfoKHR> {
	public bool Clipped;
	public SwapchainCreateFlagsKHR Flags;
	public SurfaceKHR Surface;
	public CompositeAlphaFlagsKHR CompositeAlpha;
	public Extent2D ImageExtent;
	public Format ImageFormat;
	public ImageUsageFlags ImageUsage;
	public SwapchainKHR OldSwapchain;
	public PresentModeKHR PresentMode;
	public SurfaceTransformFlagsKHR PreTransform;
	public uint ImageArrayLayers;
	public ColorSpaceKHR ImageColorSpace;
	public SharingMode ImageSharingMode;
	public uint MinImageCount;
	public uint[] QueueFamilyIndices = Array.Empty<uint>();

	public unsafe ManagedResourceSet<SwapchainCreateInfoKHR> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<SwapchainCreateInfoKHR>(new SwapchainCreateInfoKHR {
			SType = StructureType.SwapchainCreateInfoKhr,
			Clipped = Clipped,
			Flags = Flags,
			Surface = Surface,
			CompositeAlpha = CompositeAlpha,
			ImageExtent = ImageExtent,
			ImageFormat = ImageFormat,
			ImageUsage = ImageUsage,
			OldSwapchain = OldSwapchain,
			PresentMode = PresentMode,
			PreTransform = PreTransform,
			ImageArrayLayers = ImageArrayLayers,
			ImageColorSpace = ImageColorSpace,
			ImageSharingMode = ImageSharingMode,
			MinImageCount = MinImageCount,
			QueueFamilyIndexCount = (uint)QueueFamilyIndices.Length,
			PQueueFamilyIndices = resources.AllocateArray(QueueFamilyIndices)
		}, resources);
	}
}