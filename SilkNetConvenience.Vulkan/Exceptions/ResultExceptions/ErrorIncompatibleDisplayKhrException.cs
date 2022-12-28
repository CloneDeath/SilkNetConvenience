using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorIncompatibleDisplayKhrException : ResultFailureException { public ErrorIncompatibleDisplayKhrException() : base(Result.ErrorIncompatibleDisplayKhr){}}