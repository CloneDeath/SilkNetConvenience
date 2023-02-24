using Silk.NET.Core.Contexts;
using Silk.NET.Core.Native;

namespace SilkNetConvenience.KHR; 

public static unsafe class VkSurfaceExtensions {
	public static string[] GetRequiredExtensions(this IVkSurface self) {
		var glfwExtensions = self.GetRequiredExtensions(out var glfwExtensionCount);
		return SilkMarshal.PtrToStringArray((nint)glfwExtensions, (int)glfwExtensionCount);
	}
}