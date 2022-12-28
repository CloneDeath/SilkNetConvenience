using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorImageUsageNotSupportedKhrException : ResultFailureException { public ErrorImageUsageNotSupportedKhrException() : base(Result.ErrorImageUsageNotSupportedKhr){}}