using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorIncompatibleDriverException : ResultFailureException { public ErrorIncompatibleDriverException() : base(Result.ErrorIncompatibleDriver){}}