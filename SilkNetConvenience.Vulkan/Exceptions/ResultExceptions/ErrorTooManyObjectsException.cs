using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorTooManyObjectsException : ResultFailureException { public ErrorTooManyObjectsException() : base(Result.ErrorTooManyObjects){}}