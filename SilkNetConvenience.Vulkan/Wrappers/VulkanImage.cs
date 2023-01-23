using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo.Images;

namespace SilkNetConvenience.Wrappers; 

public class VulkanImage : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly Image Image;
	
	public VulkanImage(VulkanDevice device, Image image) : this(device.Vk, device.Device, image) {}
	public VulkanImage(Vk vk, Device device, Image image) {
		Vk = vk;
		Device = device;
		Image = image;
	}
	
	public VulkanImage(VulkanDevice device, ImageCreateInformation createInfo) : this(device.Vk, device.Device, createInfo){}
	public VulkanImage(Vk vk, Device device, ImageCreateInformation createInfo) {
		Vk = vk;
		Device = device;
		Image = vk.CreateImage(device, createInfo);
	}
	protected override void ReleaseVulkanResources() {
		Vk.DestroyImage(Device, Image);
	}

	public MemoryRequirements GetMemoryRequirements() {
		return Vk.GetImageMemoryRequirements(Device, Image);
	}

	public void BindMemory(VulkanDeviceMemory memory, ulong memoryOffset = 0) => BindMemory(memory.DeviceMemory, memoryOffset);

	public void BindMemory(DeviceMemory memory, ulong memoryOffset = 0) {
		Vk.BindImageMemory(Device, Image, memory, memoryOffset);
	}
}