using Silk.NET.Vulkan;
using SilkNetConvenience.Devices;

namespace SilkNetConvenience.Images; 

public class VulkanSampler : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly Sampler Sampler;

	public VulkanSampler(VulkanDevice device, SamplerCreateInformation createInfo)
		: this(device.Vk, device.Device, createInfo) {
		device.AddChildResource(this);
	}
	public VulkanSampler(Vk vk, Device device, SamplerCreateInformation createInfo) {
		Vk = vk;
		Device = device;
		Sampler = vk.CreateSampler(device, createInfo);
	}
	
	protected override void ReleaseVulkanResources() {
		Vk.DestroySampler(Device, Sampler);
	}
	
	public static implicit operator Sampler(VulkanSampler self) => self.Sampler;
}