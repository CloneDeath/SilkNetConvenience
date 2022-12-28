using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorOutOfHostMemoryException : ResultFailureException { public ErrorOutOfHostMemoryException() : base(Result.ErrorOutOfHostMemory){}}