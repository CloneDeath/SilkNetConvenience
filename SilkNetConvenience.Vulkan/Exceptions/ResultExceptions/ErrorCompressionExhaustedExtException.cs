using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorCompressionExhaustedExtException : VulkanResultException { public ErrorCompressionExhaustedExtException() : base(Result.ErrorCompressionExhaustedExt){}}