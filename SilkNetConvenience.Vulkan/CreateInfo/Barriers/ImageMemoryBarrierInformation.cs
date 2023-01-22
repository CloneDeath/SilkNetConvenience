using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Barriers; 

public class ImageMemoryBarrierInformation {
	public Image Image;
	public ImageLayout NewLayout;
	public ImageLayout OldLayout;
	public ImageSubresourceRange SubresourceRange;
	public AccessFlags DstAccessMask;
	public AccessFlags SrcAccessMask;
	public uint DstQueueFamilyIndex;
	public uint SrcQueueFamilyIndex;

	public ImageMemoryBarrier GetBarrier() {
		return new ImageMemoryBarrier {
			SType = StructureType.ImageMemoryBarrier,
			Image = Image,
			NewLayout = NewLayout,
			OldLayout = OldLayout,
			SubresourceRange = SubresourceRange,
			DstAccessMask = DstAccessMask,
			SrcAccessMask = SrcAccessMask,
			DstQueueFamilyIndex = DstQueueFamilyIndex,
			SrcQueueFamilyIndex = SrcQueueFamilyIndex
		};
	}
}