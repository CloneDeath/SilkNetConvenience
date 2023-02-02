using System;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.EXT;
using SilkNetConvenience.Instances;

namespace SilkNetConvenience.EXT; 

public class VulkanDebugUtils : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Instance Instance;
	public readonly ExtDebugUtils ExtDebugUtils;

	public VulkanDebugUtils(VulkanInstance instance) : this(instance.Vk, instance.Instance) {
		instance.AddChildResource(this);
	}
	public VulkanDebugUtils(Vk vk, Instance instance) {
		Vk = vk;
		Instance = instance;
		ExtDebugUtils = vk.GetDebugUtilsExtension(instance) ?? throw new NullReferenceException();
	}

	protected override void ReleaseVulkanResources() { }
	
	public static implicit operator ExtDebugUtils(VulkanDebugUtils self) => self.ExtDebugUtils;

	public VulkanDebugUtilsMessenger CreateDebugUtilsMessenger(DebugUtilsMessengerCreateInformation createInfo) => new(this, createInfo);
}