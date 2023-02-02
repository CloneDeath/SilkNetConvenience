using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorOutOfDateKhrException : VulkanResultException { public ErrorOutOfDateKhrException() : base(Result.ErrorOutOfDateKhr){}}