using System.Linq;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.EXT;
using Silk.NET.Vulkan.Extensions.KHR;
using SilkNetConvenience.CreateInfo;

namespace SilkNetConvenience.Wrappers; 

public class VulkanInstance : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Instance Instance;

	public VulkanInstance(VulkanContext vk, InstanceCreateInformation createInfo) : this(vk.Vk, createInfo){}
	public VulkanInstance(Vk vk, InstanceCreateInformation createInfo) {
		Vk = vk;
		Instance = vk.CreateInstance(createInfo);
	}
	
	protected override void ReleaseVulkanResources() {
		Vk.DestroyInstance(Instance);
	}

	public ExtDebugUtils? GetDebugUtilsExtension() => Vk.GetDebugUtilsExtension(Instance);
	public KhrSurface? GetKhrSurfaceExtension() => Vk.GetKhrSurfaceExtension(Instance);
	public VulkanPhysicalDevice[] EnumeratePhysicalDevices() => Vk.EnumeratePhysicalDevices(Instance)
		.Select(p => new VulkanPhysicalDevice(this, p))
		.ToArray();
}