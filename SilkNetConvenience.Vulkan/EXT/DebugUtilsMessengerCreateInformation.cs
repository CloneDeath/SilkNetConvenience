using Silk.NET.Vulkan;

namespace SilkNetConvenience.EXT; 

public class DebugUtilsMessengerCreateInformation : IGetCreateInfo<DebugUtilsMessengerCreateInfoEXT> {
	public DebugUtilsMessageSeverityFlagsEXT MessageSeverity;
	public DebugUtilsMessageTypeFlagsEXT MessageType;
	public PfnDebugUtilsMessengerCallbackEXT PfnUserCallback;

	public ManagedResourceSet<DebugUtilsMessengerCreateInfoEXT> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<DebugUtilsMessengerCreateInfoEXT>(new DebugUtilsMessengerCreateInfoEXT {
			SType = StructureType.DebugUtilsMessengerCreateInfoExt,
			MessageSeverity = MessageSeverity,
			MessageType = MessageType,
			PfnUserCallback = PfnUserCallback
		}, resources);
	}
}