using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions; 

public abstract class ResultFailureException : Exception {
	public Result Result { get; }
	
	protected ResultFailureException(Result result) : base(result.ToString()) {
		Result = result;
	}
}