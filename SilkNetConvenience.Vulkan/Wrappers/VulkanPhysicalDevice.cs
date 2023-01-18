using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo;

namespace SilkNetConvenience.Wrappers; 

public class VulkanPhysicalDevice {
	public readonly Vk Vk;
	public readonly Instance Instance;
	public readonly PhysicalDevice PhysicalDevice;
	
	public VulkanPhysicalDevice(VulkanInstance instance, PhysicalDevice physicalDevice) : this(instance.Vk, instance.Instance, physicalDevice){}
	public VulkanPhysicalDevice(Vk vk, Instance instance, PhysicalDevice physicalDevice) {
		Vk = vk;
		Instance = instance;
		PhysicalDevice = physicalDevice;
	}

	public ExtensionProperties[] EnumerateExtensionProperties() => Vk.EnumerateDeviceExtensionProperties(PhysicalDevice);
	public QueueFamilyProperties[] GetQueueFamilyProperties() => Vk.GetPhysicalDeviceQueueFamilyProperties(PhysicalDevice);
	public PhysicalDeviceMemoryProperties GetMemoryProperties() => Vk.GetPhysicalDeviceMemoryProperties(PhysicalDevice);

	public VulkanDevice CreateDevice(DeviceCreateInformation createInfo) => new(this, createInfo);
}