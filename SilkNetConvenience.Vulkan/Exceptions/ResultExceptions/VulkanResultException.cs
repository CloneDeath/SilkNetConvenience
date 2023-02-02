using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions; 

public abstract class VulkanResultException : Exception {
	public Result Result { get; }
	
	protected VulkanResultException(Result result) : base(result.ToString()) {
		Result = result;
	}
}