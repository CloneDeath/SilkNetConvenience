using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorVideoStdVersionNotSupportedKhrException : ResultFailureException { public ErrorVideoStdVersionNotSupportedKhrException() : base(Result.ErrorVideoStdVersionNotSupportedKhr){}}