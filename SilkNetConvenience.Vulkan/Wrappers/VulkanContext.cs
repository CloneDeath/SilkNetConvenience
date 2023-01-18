using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;

namespace SilkNetConvenience.Wrappers; 

public class VulkanContext : BaseVulkanWrapper {
	public readonly Vk Vk;
	
	public VulkanContext() : this(Vk.GetApi()) {}
	public VulkanContext(Vk vk) {
		Vk = vk;
	}

	protected override void ReleaseVulkanResources() {
		Vk.Dispose();
	}

	public VulkanInstance CreateInstance(InstanceCreateInformation createInfo) => new(this, createInfo);

	public LayerProperties[] EnumerateInstanceLayerProperties() => Vk.EnumerateInstanceLayerProperties();
}