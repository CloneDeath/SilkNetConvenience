using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorFormatNotSupportedException : ResultFailureException { public ErrorFormatNotSupportedException() : base(Result.ErrorFormatNotSupported){}}