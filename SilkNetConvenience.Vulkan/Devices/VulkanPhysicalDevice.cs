using Silk.NET.Vulkan;
using SilkNetConvenience.Instances;

namespace SilkNetConvenience.Devices; 

public class VulkanPhysicalDevice : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Instance Instance;
	public readonly PhysicalDevice PhysicalDevice;

	public VulkanPhysicalDevice(VulkanInstance instance, PhysicalDevice physicalDevice) : this(
		instance.Vk, instance.Instance, physicalDevice) {
		instance.AddChildResource(this);
	}
	public VulkanPhysicalDevice(Vk vk, Instance instance, PhysicalDevice physicalDevice) {
		Vk = vk;
		Instance = instance;
		PhysicalDevice = physicalDevice;
	}

	protected override void ReleaseVulkanResources() { }

	public static implicit operator PhysicalDevice(VulkanPhysicalDevice self) => self.PhysicalDevice;

	public ExtensionProperties[] EnumerateExtensionProperties() => Vk.EnumerateDeviceExtensionProperties(PhysicalDevice);

	public QueueFamilyProperties[] GetQueueFamilyProperties() => Vk.GetPhysicalDeviceQueueFamilyProperties(PhysicalDevice);

	public PhysicalDeviceMemoryProperties GetMemoryProperties() => Vk.GetPhysicalDeviceMemoryProperties(PhysicalDevice);

	public VulkanDevice CreateDevice(DeviceCreateInformation createInfo) => new(this, createInfo);

	public PhysicalDeviceProperties GetProperties() => Vk.GetPhysicalDeviceProperties(PhysicalDevice);

	public PhysicalDeviceFeatures GetFeatures() => Vk.GetPhysicalDeviceFeatures(PhysicalDevice);

	public FormatProperties GetFormatProperties(Format format) => Vk.GetPhysicalDeviceFormatProperties(PhysicalDevice, format);
}