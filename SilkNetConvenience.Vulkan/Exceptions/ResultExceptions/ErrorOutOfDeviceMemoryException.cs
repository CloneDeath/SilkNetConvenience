using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorOutOfDeviceMemoryException : ResultFailureException { public ErrorOutOfDeviceMemoryException() : base(Result.ErrorOutOfDeviceMemory){}}