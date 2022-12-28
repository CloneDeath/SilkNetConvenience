using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorOutOfPoolMemoryKhrException : ResultFailureException { public ErrorOutOfPoolMemoryKhrException() : base(Result.ErrorOutOfPoolMemoryKhr){}}