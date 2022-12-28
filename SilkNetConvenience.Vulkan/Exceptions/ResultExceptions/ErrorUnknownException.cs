using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorUnknownException : ResultFailureException { public ErrorUnknownException() : base(Result.ErrorUnknown){}}