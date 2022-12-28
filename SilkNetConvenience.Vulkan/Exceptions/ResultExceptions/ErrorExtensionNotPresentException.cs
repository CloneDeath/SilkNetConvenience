using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorExtensionNotPresentException : ResultFailureException { public ErrorExtensionNotPresentException() : base(Result.ErrorExtensionNotPresent){}}