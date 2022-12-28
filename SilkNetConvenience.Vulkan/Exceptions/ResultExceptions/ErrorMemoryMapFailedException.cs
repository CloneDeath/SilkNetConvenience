using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorMemoryMapFailedException : ResultFailureException { public ErrorMemoryMapFailedException() : base(Result.ErrorMemoryMapFailed){}}