using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorOutOfHostMemoryException : VulkanResultException { public ErrorOutOfHostMemoryException() : base(Result.ErrorOutOfHostMemory){}}