using Silk.NET.Vulkan;

namespace SilkNetConvenience; 

public static class LayerPropertiesExtensions {
	public static unsafe string GetLayerName(this LayerProperties self) => Helpers.GetString(self.LayerName);
}