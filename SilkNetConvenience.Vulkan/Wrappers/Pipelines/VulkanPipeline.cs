using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo.Pipelines;

namespace SilkNetConvenience.Wrappers.Pipelines; 

public class VulkanPipeline : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly Pipeline Pipeline;

	public PipelineBindPoint PipelineBindPoint { get; }

	public VulkanPipeline(VulkanDevice device, GraphicsPipelineCreateInformation createInfo)
		: this(device.Vk, device.Device, createInfo) {
		device.AddChildResource(this);
	}
	public VulkanPipeline(Vk vk, Device device, GraphicsPipelineCreateInformation createInfo) {
		Vk = vk;
		Device = device;
		PipelineBindPoint = PipelineBindPoint.Graphics;
		Pipeline = vk.CreateGraphicsPipeline(device, createInfo);
	}

	protected override void ReleaseVulkanResources() {
		Vk.DestroyPipeline(Device, Pipeline);
	}
	
	public static implicit operator Pipeline(VulkanPipeline self) => self.Pipeline;
}