using Silk.NET.Core;
using Silk.NET.Vulkan;

namespace SilkNetConvenience; 

public class ApplicationInformation : IGetCreateInfo<ApplicationInfo> {
	public string ApplicationName = string.Empty;
	public string EngineName = string.Empty;
	public Version32 ApiVersion = new(1, 0, 0);
	public Version32 ApplicationVersion = new(1, 0, 0);
	public Version32 EngineVersion = new(1, 0, 0);

	public unsafe ManagedResourceSet<ApplicationInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<ApplicationInfo>(new ApplicationInfo {
			SType = StructureType.ApplicationInfo,
			ApiVersion = ApiVersion,
			ApplicationVersion = ApplicationVersion,
			EngineVersion = EngineVersion,
			PApplicationName = resources.AllocateString(ApplicationName),
			PEngineName = resources.AllocateString(EngineName)
		}, resources);
	}
}