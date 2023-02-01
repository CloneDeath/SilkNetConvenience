using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.EXT;
using SilkNetConvenience.CreateInfo.EXT;

namespace SilkNetConvenience.Wrappers; 

public class VulkanDebugUtilsMessenger : BaseVulkanWrapper {
	public Vk Vk;
	public Instance Instance;
	public ExtDebugUtils ExtDebugUtils;
	public DebugUtilsMessengerEXT DebugUtilsMessengerEXT;

	public VulkanDebugUtilsMessenger(VulkanDebugUtils debugUtils, DebugUtilsMessengerCreateInformation createInfo)
		: this(debugUtils.Vk, debugUtils.Instance, debugUtils.ExtDebugUtils, createInfo) {
		debugUtils.AddChildResource(this);
	}

	public VulkanDebugUtilsMessenger(Vk vk, Instance instance, ExtDebugUtils extDebugUtils,
									 DebugUtilsMessengerCreateInformation createInfo) {
		Vk = vk;
		Instance = instance;
		ExtDebugUtils = extDebugUtils;
		DebugUtilsMessengerEXT = extDebugUtils.CreateDebugUtilsMessenger(instance, createInfo);
	}

	protected override void ReleaseVulkanResources() {
		ExtDebugUtils.DestroyDebugUtilsMessenger(Instance, DebugUtilsMessengerEXT);
	}
}