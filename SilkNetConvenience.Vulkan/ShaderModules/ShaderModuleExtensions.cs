using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.ShaderModules; 

public static unsafe class ShaderModuleExtensions {
	public static ShaderModule CreateShaderModule(this Vk vk, Device device, ShaderModuleCreateInformation createInfo) {
		using var info = createInfo.GetCreateInfo();
		vk.CreateShaderModule(device, info.Resource, null, out var shaderModule).AssertSuccess();
		return shaderModule;
	}

	public static void DestroyShaderModule(this Vk vk, Device device, ShaderModule shaderModule) {
		vk.DestroyShaderModule(device, shaderModule, null);
	}
}