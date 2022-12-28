using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorInvalidExternalHandleKhrException : ResultFailureException { public ErrorInvalidExternalHandleKhrException() : base(Result.ErrorInvalidExternalHandleKhr){}}