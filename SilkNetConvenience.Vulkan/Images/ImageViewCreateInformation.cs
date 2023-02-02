using Silk.NET.Vulkan;

namespace SilkNetConvenience.Images; 

public class ImageViewCreateInformation : IGetCreateInfo<ImageViewCreateInfo> {
	public Image Image;
	public ComponentMapping Components;
	public ImageViewCreateFlags Flags;
	public Format Format;
	public ImageSubresourceRange SubresourceRange;
	public ImageViewType ViewType;

	public ManagedResourceSet<ImageViewCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<ImageViewCreateInfo>(new ImageViewCreateInfo {
			SType = StructureType.ImageViewCreateInfo,
			Image = Image,
			Components = Components,
			Flags = Flags,
			Format = Format,
			SubresourceRange = SubresourceRange,
			ViewType = ViewType
		}, resources);
	}
}