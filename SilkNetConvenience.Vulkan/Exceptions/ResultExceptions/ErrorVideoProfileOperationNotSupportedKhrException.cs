using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorVideoProfileOperationNotSupportedKhrException : ResultFailureException { public ErrorVideoProfileOperationNotSupportedKhrException() : base(Result.ErrorVideoProfileOperationNotSupportedKhr){}}