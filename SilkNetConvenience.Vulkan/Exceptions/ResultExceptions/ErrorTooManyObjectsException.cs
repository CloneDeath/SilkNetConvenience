using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorTooManyObjectsException : VulkanResultException { public ErrorTooManyObjectsException() : base(Result.ErrorTooManyObjects){}}