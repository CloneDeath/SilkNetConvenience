using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorDeviceLostException : ResultFailureException { public ErrorDeviceLostException() : base(Result.ErrorDeviceLost){}}