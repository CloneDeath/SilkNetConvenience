using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorFullScreenExclusiveModeLostExtException : ResultFailureException { public ErrorFullScreenExclusiveModeLostExtException() : base(Result.ErrorFullScreenExclusiveModeLostExt){}}