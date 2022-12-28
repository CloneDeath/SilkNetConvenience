using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorValidationFailedExtException : ResultFailureException { public ErrorValidationFailedExtException() : base(Result.ErrorValidationFailedExt){}}