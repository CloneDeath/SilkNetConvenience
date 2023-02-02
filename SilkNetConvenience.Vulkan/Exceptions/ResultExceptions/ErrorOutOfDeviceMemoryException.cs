using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorOutOfDeviceMemoryException : VulkanResultException { public ErrorOutOfDeviceMemoryException() : base(Result.ErrorOutOfDeviceMemory){}}