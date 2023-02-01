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
	
	public static implicit operator PhysicalDevice(VulkanPhysicalDevice self) => self.PhysicalDevice;

	public ExtensionProperties[] EnumerateExtensionProperties() => Vk.EnumerateDeviceExtensionProperties(PhysicalDevice);
	public QueueFamilyProperties[] GetQueueFamilyProperties() => Vk.GetPhysicalDeviceQueueFamilyProperties(PhysicalDevice);
	public PhysicalDeviceMemoryProperties GetMemoryProperties() => Vk.GetPhysicalDeviceMemoryProperties(PhysicalDevice);

	public VulkanDevice CreateDevice(DeviceCreateInformation createInfo) => new(this, createInfo);

	public PhysicalDeviceProperties GetProperties() => Vk.GetPhysicalDeviceProperties(PhysicalDevice);

	public PhysicalDeviceFeatures GetFeatures() => Vk.GetPhysicalDeviceFeatures(PhysicalDevice);

	public FormatProperties GetFormatProperties(Format format) => Vk.GetPhysicalDeviceFormatProperties(PhysicalDevice, format);
}