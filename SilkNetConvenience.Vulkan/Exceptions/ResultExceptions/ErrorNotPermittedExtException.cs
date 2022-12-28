using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorNotPermittedExtException : ResultFailureException { public ErrorNotPermittedExtException() : base(Result.ErrorNotPermittedExt){}}