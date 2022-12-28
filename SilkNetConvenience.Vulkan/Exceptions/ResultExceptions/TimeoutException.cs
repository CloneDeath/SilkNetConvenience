using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class TimeoutException : ResultFailureException { public TimeoutException() : base(Result.Timeout){}}