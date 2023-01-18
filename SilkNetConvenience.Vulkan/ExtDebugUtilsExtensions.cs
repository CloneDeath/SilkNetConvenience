using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.EXT;
using SilkNetConvenience.CreateInfo.EXT;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience; 

public static unsafe class ExtDebugUtilsExtensions {
	public static DebugUtilsMessengerEXT CreateDebugUtilsMessenger(this ExtDebugUtils debugUtils, Instance instance,
		DebugUtilsMessengerCreateInformation createInformation) {
		debugUtils.CreateDebugUtilsMessenger(instance, createInformation.GetCreateInfo(), 
			null, out var debugMessenger).AssertSuccess();
		return debugMessenger;
	}
}