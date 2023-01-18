using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.Exceptions;
using Buffer = Silk.NET.Vulkan.Buffer;

namespace SilkNetConvenience.Wrappers; 

public class VulkanBuffer : BaseVulkanWrapper {
	private readonly Vk _vk;
	private readonly Device _device;
	public readonly Buffer Buffer;

	public VulkanBuffer(VulkanDevice device, BufferCreateInformation createInfo) : this(device.Vk, device.Device, createInfo){}
	public VulkanBuffer(Vk vk, Device device, BufferCreateInformation createInfo) {
		_vk = vk;
		_device = device;
		Buffer = vk.CreateBuffer(device, createInfo);
	}
	
	protected override void ReleaseVulkanResources() {
		_vk.DestroyBuffer(_device, Buffer);
	}

	public void BindMemory(VulkanDeviceMemory deviceMemory, ulong memoryOffset = 0) => BindMemory(deviceMemory.DeviceMemory, memoryOffset);
	public void BindMemory(DeviceMemory deviceMemory, ulong memoryOffset = 0) {
		_vk.BindBufferMemory(_device, Buffer, deviceMemory, memoryOffset).AssertSuccess();
	}
}