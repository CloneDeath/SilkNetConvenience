using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorLayerNotPresentException : ResultFailureException { public ErrorLayerNotPresentException() : base(Result.ErrorLayerNotPresent){}}