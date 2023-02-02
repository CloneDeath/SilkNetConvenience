using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorFullScreenExclusiveModeLostExtException : VulkanResultException { public ErrorFullScreenExclusiveModeLostExtException() : base(Result.ErrorFullScreenExclusiveModeLostExt){}}