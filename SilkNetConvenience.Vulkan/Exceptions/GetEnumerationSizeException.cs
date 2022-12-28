using System;
using System.Reflection;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions; 

public class GetEnumerationSizeException : Exception {
	public GetEnumerationSizeException(MemberInfo type, Result result) 
		: base($"Failed to get number of {type.Name}, Result: {result}."){}
}