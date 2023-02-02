using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorInvalidShaderNVException : VulkanResultException { public ErrorInvalidShaderNVException() : base(Result.ErrorInvalidShaderNV){}}