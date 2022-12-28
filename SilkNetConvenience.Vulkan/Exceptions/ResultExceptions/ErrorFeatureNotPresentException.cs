using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorFeatureNotPresentException : ResultFailureException { public ErrorFeatureNotPresentException() : base(Result.ErrorFeatureNotPresent){}}