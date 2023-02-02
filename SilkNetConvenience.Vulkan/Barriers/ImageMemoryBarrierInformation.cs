using Silk.NET.Vulkan;

namespace SilkNetConvenience.Barriers; 

public class ImageMemoryBarrierInformation : IGetCreateInfo<ImageMemoryBarrier> {
	public Image Image;
	public ImageLayout NewLayout;
	public ImageLayout OldLayout;
	public ImageSubresourceRange SubresourceRange;
	public AccessFlags DstAccessMask;
	public AccessFlags SrcAccessMask;
	public uint DstQueueFamilyIndex;
	public uint SrcQueueFamilyIndex;

	public ManagedResourceSet<ImageMemoryBarrier> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<ImageMemoryBarrier>(new ImageMemoryBarrier {
			SType = StructureType.ImageMemoryBarrier,
			Image = Image,
			NewLayout = NewLayout,
			OldLayout = OldLayout,
			SubresourceRange = SubresourceRange,
			DstAccessMask = DstAccessMask,
			SrcAccessMask = SrcAccessMask,
			DstQueueFamilyIndex = DstQueueFamilyIndex,
			SrcQueueFamilyIndex = SrcQueueFamilyIndex
		}, resources);
	}
}