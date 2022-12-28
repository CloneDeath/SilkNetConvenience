using System;

namespace SilkNetConvenience; 

public class InstanceCreateInformation {
	public ApplicationInformation ApplicationInfo = new();
	public string[] EnabledExtensions = Array.Empty<string>();
	public string[] EnabledLayerNames = Array.Empty<string>();
}