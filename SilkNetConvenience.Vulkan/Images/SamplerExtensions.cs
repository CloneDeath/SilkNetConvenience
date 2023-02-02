using Silk.NET.Vulkan;
using SilkNetConvenience.Exceptions;

namespace SilkNetConvenience.Images; 

public static unsafe class SamplerExtensions {
	public static Sampler CreateSampler(this Vk vk, Device device, SamplerCreateInformation createInfo) {
		using var info = createInfo.GetCreateInfo();
		vk.CreateSampler(device, info.Resource, null, out var sampler).AssertSuccess();
		return sampler;
	}

	public static void DestroySampler(this Vk vk, Device device, Sampler sampler) {
		vk.DestroySampler(device, sampler, null);
	}
}