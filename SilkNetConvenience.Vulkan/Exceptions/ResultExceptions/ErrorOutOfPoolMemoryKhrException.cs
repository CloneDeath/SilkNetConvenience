using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorOutOfPoolMemoryKhrException : VulkanResultException { public ErrorOutOfPoolMemoryKhrException() : base(Result.ErrorOutOfPoolMemoryKhr){}}