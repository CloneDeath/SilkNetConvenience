using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorNativeWindowInUseKhrException : VulkanResultException { public ErrorNativeWindowInUseKhrException() : base(Result.ErrorNativeWindowInUseKhr){}}