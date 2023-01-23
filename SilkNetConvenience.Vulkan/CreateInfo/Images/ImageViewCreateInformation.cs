using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Images; 

public class ImageViewCreateInformation {
	public Image Image;
	public ComponentMapping Components;
	public ImageViewCreateFlags Flags;
	public Format Format;
	public ImageSubresourceRange SubresourceRange;
	public ImageViewType ViewType;

	public ImageViewCreateInfo GetCreateInfo() {
		return new ImageViewCreateInfo {
			SType = StructureType.ImageViewCreateInfo,
			Image = Image,
			Components = Components,
			Flags = Flags,
			Format = Format,
			SubresourceRange = SubresourceRange,
			ViewType = ViewType
		};
	}
}