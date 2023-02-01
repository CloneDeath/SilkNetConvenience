using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Images; 

public class ImageCreateInformation : IGetCreateInfo<ImageCreateInfo> {
	public Extent3D Extent;
	public ImageCreateFlags Flags;
	public Format Format;
	public SampleCountFlags Samples;
	public ImageTiling Tiling;
	public ImageUsageFlags Usage;
	public uint ArrayLayers;
	public ImageType ImageType;
	public ImageLayout InitialLayout;
	public uint MipLevels;
	public SharingMode SharingMode;
	public uint[] QueueFamilyIndices = Array.Empty<uint>();

	public unsafe ManagedResourceSet<ImageCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<ImageCreateInfo>(new ImageCreateInfo {
			SType = StructureType.ImageCreateInfo,
			Extent = Extent,
			Flags = Flags,
			Format = Format,
			Samples = Samples,
			Tiling = Tiling,
			Usage = Usage,
			ArrayLayers = ArrayLayers,
			ImageType = ImageType,
			InitialLayout = InitialLayout,
			MipLevels = MipLevels,
			SharingMode = SharingMode,
			QueueFamilyIndexCount = (uint)QueueFamilyIndices.Length,
			PQueueFamilyIndices = resources.AllocateArray(QueueFamilyIndices)
		}, resources);
	}
}