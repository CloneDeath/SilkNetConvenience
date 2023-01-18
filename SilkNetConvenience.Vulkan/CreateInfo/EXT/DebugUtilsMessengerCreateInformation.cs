using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.EXT; 

public class DebugUtilsMessengerCreateInformation {
	public DebugUtilsMessageSeverityFlagsEXT MessageSeverity;
	public DebugUtilsMessageTypeFlagsEXT MessageType;
	public PfnDebugUtilsMessengerCallbackEXT PfnUserCallback;

	public DebugUtilsMessengerCreateInfoEXT GetCreateInfo() {
		return new DebugUtilsMessengerCreateInfoEXT {
			SType = StructureType.DebugUtilsMessengerCreateInfoExt,
			MessageSeverity = MessageSeverity,
			MessageType = MessageType,
			PfnUserCallback = PfnUserCallback
		};
	}
}