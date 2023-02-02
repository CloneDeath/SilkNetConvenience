using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorMemoryMapFailedException : VulkanResultException { public ErrorMemoryMapFailedException() : base(Result.ErrorMemoryMapFailed){}}