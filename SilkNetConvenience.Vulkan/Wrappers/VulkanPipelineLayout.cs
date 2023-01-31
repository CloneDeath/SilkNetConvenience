using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo.Pipelines;

namespace SilkNetConvenience.Wrappers; 

public class VulkanPipelineLayout : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly PipelineLayout PipelineLayout;

	public VulkanPipelineLayout(VulkanDevice device, PipelineLayoutCreateInformation createInfo)
		: this(device.Vk, device.Device, createInfo) {
		device.AddChildResource(this);
	}
	public VulkanPipelineLayout(Vk vk, Device device, PipelineLayoutCreateInformation createInfo) {
		Vk = vk;
		Device = device;
		PipelineLayout = vk.CreatePipelineLayout(device, createInfo);
	}
	
	protected override void ReleaseVulkanResources() {
		Vk.DestroyPipelineLayout(Device, PipelineLayout);
	}
}