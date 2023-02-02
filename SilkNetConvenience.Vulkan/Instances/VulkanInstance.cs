using System.Linq;
using Silk.NET.Vulkan;
using SilkNetConvenience.Devices;
using SilkNetConvenience.EXT;
using SilkNetConvenience.KHR;

namespace SilkNetConvenience.Instances; 

public class VulkanInstance : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Instance Instance;

	public VulkanDebugUtils DebugUtils { get; }
	public VulkanKhrSurface KhrSurface { get; }

	public VulkanInstance(VulkanContext vk, InstanceCreateInformation createInfo) : this(vk.Vk, createInfo) {
		vk.AddChildResource(this);
	}

	public VulkanInstance(Vk vk, InstanceCreateInformation createInfo) {
		Vk = vk;
		Instance = vk.CreateInstance(createInfo);
		DebugUtils = new VulkanDebugUtils(this);
		KhrSurface = new VulkanKhrSurface(this);
	}

	protected override void ReleaseVulkanResources() {
		Vk.DestroyInstance(Instance);
	}

	public static implicit operator Instance(VulkanInstance self) => self.Instance;

	public VulkanPhysicalDevice[] EnumeratePhysicalDevices() => Vk.EnumeratePhysicalDevices(Instance)
																  .Select(p => new VulkanPhysicalDevice(this, p))
																  .ToArray();
}