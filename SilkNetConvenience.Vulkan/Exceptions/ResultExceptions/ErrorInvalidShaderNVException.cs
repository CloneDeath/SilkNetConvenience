using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorInvalidShaderNVException : ResultFailureException { public ErrorInvalidShaderNVException() : base(Result.ErrorInvalidShaderNV){}}