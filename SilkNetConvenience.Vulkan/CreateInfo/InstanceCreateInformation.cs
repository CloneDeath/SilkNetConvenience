using System;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo.EXT;
using SilkNetConvenience.CreateInfo.Pipelines;

namespace SilkNetConvenience.CreateInfo; 

public class InstanceCreateInformation : IGetCreateInfo<InstanceCreateInfo> {
	public ApplicationInformation ApplicationInfo = new();
	public string[] EnabledExtensions = Array.Empty<string>();
	public string[] EnabledLayers = Array.Empty<string>();
	public InstanceCreateFlags Flags;
	
	public DebugUtilsMessengerCreateInformation? DebugUtilsMessengerCreateInfo;

	public unsafe ManagedResourceSet<InstanceCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		var createInfo = new InstanceCreateInfo {
			SType = StructureType.InstanceCreateInfo,
			EnabledExtensionCount = (uint)EnabledExtensions.Length,
			PpEnabledExtensionNames = resources.AllocateStringArray(EnabledExtensions),
			EnabledLayerCount = (uint)EnabledLayers.Length,
			PpEnabledLayerNames = resources.AllocateStringArray(EnabledLayers),
			PApplicationInfo = resources.AllocateCreateInfo(ApplicationInfo),
			Flags = Flags
		};
		if (DebugUtilsMessengerCreateInfo != null) {
			createInfo.PNext = resources.AllocateCreateInfo(DebugUtilsMessengerCreateInfo);
		}
		return new ManagedResourceSet<InstanceCreateInfo>(createInfo, resources);
	}
}