using System;
using System.Reflection;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions; 

public class GetEnumerationDataException : Exception {
	public GetEnumerationDataException(MemberInfo type, Result result) 
		: base($"Failed to get data for {type.Name}, Result: {result}."){}
}