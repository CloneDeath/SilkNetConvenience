using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorIncompatibleDriverException : VulkanResultException { public ErrorIncompatibleDriverException() : base(Result.ErrorIncompatibleDriver){}}