using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorSurfaceLostKhrException : ResultFailureException { public ErrorSurfaceLostKhrException() : base(Result.ErrorSurfaceLostKhr){}}