using System.Linq;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.EXT;
using Silk.NET.Vulkan.Extensions.KHR;
using SilkNetConvenience.CreateInfo;
using SilkNetConvenience.CreateInfo.EXT;

namespace SilkNetConvenience.Wrappers; 

public class VulkanInstance : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Instance Instance;

	public VulkanInstance(VulkanContext vk, InstanceCreateInformation createInfo) : this(vk.Vk, createInfo) {
		vk.AddChildResource(this);
	}
	public VulkanInstance(Vk vk, InstanceCreateInformation createInfo) {
		Vk = vk;
		Instance = vk.CreateInstance(createInfo);
	}
	
	protected override void ReleaseVulkanResources() {
		Vk.DestroyInstance(Instance);
	}

	public VulkanDebugUtils GetDebugUtilsExtension() => new(this);
	public KhrSurface? GetKhrSurfaceExtension() => Vk.GetKhrSurfaceExtension(Instance);
	public VulkanPhysicalDevice[] EnumeratePhysicalDevices() => Vk.EnumeratePhysicalDevices(Instance)
		.Select(p => new VulkanPhysicalDevice(this, p))
		.ToArray();
}