using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.EXT;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.EXT; 

public static unsafe class ExtDebugUtilsExtensions {
	public static DebugUtilsMessengerEXT CreateDebugUtilsMessenger(this ExtDebugUtils debugUtils, Instance instance, DebugUtilsMessengerCreateInformation createInfo) {
		using var info = createInfo.GetCreateInfo(); 
		debugUtils.CreateDebugUtilsMessenger(instance, info.Resource, null, out var debugMessenger).AssertSuccess();
		return debugMessenger;
	}

	public static void DestroyDebugUtilsMessenger(this ExtDebugUtils debugUtils, Instance instance,
												  DebugUtilsMessengerEXT debugUtilsMessenger) {
		debugUtils.DestroyDebugUtilsMessenger(instance, debugUtilsMessenger, null);
	}
}