using Silk.NET.Vulkan;
using SilkNetConvenience.Devices;
using SilkNetConvenience.Memory;

namespace SilkNetConvenience.Images; 

public class VulkanImage : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly Image Image;

	public VulkanImage(VulkanDevice device, Image image) : this(device.Vk, device.Device, image) {
		device.AddChildResource(this);
	}
	public VulkanImage(Vk vk, Device device, Image image) {
		Vk = vk;
		Device = device;
		Image = image;
	}

	public VulkanImage(VulkanDevice device, ImageCreateInformation createInfo) : this(device.Vk, device.Device, createInfo) {
		device.AddChildResource(this);
	}
	public VulkanImage(Vk vk, Device device, ImageCreateInformation createInfo) {
		Vk = vk;
		Device = device;
		Image = vk.CreateImage(device, createInfo);		
	}
	protected override void ReleaseVulkanResources() {
		Vk.DestroyImage(Device, Image);
	}
	
	public static implicit operator Image(VulkanImage self) => self.Image;

	public MemoryRequirements GetMemoryRequirements() {
		return Vk.GetImageMemoryRequirements(Device, Image);
	}

	public void BindMemory(VulkanDeviceMemory memory, ulong memoryOffset = 0) => BindMemory(memory.DeviceMemory, memoryOffset);

	public void BindMemory(DeviceMemory memory, ulong memoryOffset = 0) {
		Vk.BindImageMemory(Device, Image, memory, memoryOffset);
	}
}