using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.ShaderModules;

public class ShaderModuleCreateInformation : IGetCreateInfo<ShaderModuleCreateInfo> {
	public byte[] Code = Array.Empty<byte>();

	public unsafe ManagedResourceSet<ShaderModuleCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<ShaderModuleCreateInfo>(new ShaderModuleCreateInfo {
			SType = StructureType.ShaderModuleCreateInfo,
			CodeSize = (uint)Code.Length,
			PCode = (uint*)resources.AllocateArray(Code)
		}, resources);
	}
}