using Silk.NET.Vulkan;

namespace SilkNetConvenience.Instances; 

public static class LayerPropertiesExtensions {
	public static unsafe string GetLayerName(this LayerProperties self) => Helpers.GetString(self.LayerName);
}