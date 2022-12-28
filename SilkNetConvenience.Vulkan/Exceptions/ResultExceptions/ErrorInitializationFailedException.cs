using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorInitializationFailedException : ResultFailureException { public ErrorInitializationFailedException() : base(Result.ErrorInitializationFailed){}}