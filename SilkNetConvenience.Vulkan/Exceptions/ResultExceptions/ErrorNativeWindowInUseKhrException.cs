using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorNativeWindowInUseKhrException : ResultFailureException { public ErrorNativeWindowInUseKhrException() : base(Result.ErrorNativeWindowInUseKhr){}}