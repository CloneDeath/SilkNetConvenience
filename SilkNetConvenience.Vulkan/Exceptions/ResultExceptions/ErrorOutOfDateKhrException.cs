using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorOutOfDateKhrException : ResultFailureException { public ErrorOutOfDateKhrException() : base(Result.ErrorOutOfDateKhr){}}