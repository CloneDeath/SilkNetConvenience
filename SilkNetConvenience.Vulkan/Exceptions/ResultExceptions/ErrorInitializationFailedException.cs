using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorInitializationFailedException : VulkanResultException { public ErrorInitializationFailedException() : base(Result.ErrorInitializationFailed){}}