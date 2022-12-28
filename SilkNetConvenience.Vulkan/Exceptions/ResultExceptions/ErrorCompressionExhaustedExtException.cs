using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorCompressionExhaustedExtException : ResultFailureException { public ErrorCompressionExhaustedExtException() : base(Result.ErrorCompressionExhaustedExt){}}