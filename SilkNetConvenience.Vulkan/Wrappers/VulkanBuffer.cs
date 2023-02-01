using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;
using Buffer = Silk.NET.Vulkan.Buffer;

namespace SilkNetConvenience.Wrappers; 

public class VulkanBuffer : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly Buffer Buffer;

	public VulkanBuffer(VulkanDevice device, BufferCreateInformation createInfo) : this(device.Vk, device.Device, createInfo) {
		device.AddChildResource(this);
	}
	public VulkanBuffer(Vk vk, Device device, BufferCreateInformation createInfo) {
		Vk = vk;
		Device = device;
		Buffer = vk.CreateBuffer(device, createInfo);
	}
	
	protected override void ReleaseVulkanResources() {
		Vk.DestroyBuffer(Device, Buffer);
	}
	
	public static implicit operator Buffer(VulkanBuffer self) => self.Buffer;

	public void BindMemory(VulkanDeviceMemory deviceMemory, ulong memoryOffset = 0) => BindMemory(deviceMemory.DeviceMemory, memoryOffset);
	public void BindMemory(DeviceMemory deviceMemory, ulong memoryOffset = 0) 
		=> Vk.BindBufferMemory(Device, Buffer, deviceMemory, memoryOffset).AssertSuccess();
	public MemoryRequirements GetMemoryRequirements() => Vk.GetBufferMemoryRequirements(Device, Buffer);
}