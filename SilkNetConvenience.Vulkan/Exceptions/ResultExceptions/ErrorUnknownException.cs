using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorUnknownException : VulkanResultException { public ErrorUnknownException() : base(Result.ErrorUnknown){}}