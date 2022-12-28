using Silk.NET.Core;

namespace SilkNetConvenience.CreateInfo; 

public class ApplicationInformation {
	public string ApplicationName = string.Empty;
	public string EngineName = string.Empty;
	public Version32 ApiVersion = new(1, 0, 0);
	public Version32 ApplicationVersion = new(1, 0, 0);
	public Version32 EngineVersion = new(1, 0, 0);
}