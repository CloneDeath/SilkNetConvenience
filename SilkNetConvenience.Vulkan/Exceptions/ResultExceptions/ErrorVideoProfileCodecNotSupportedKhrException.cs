using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorVideoProfileCodecNotSupportedKhrException : ResultFailureException { public ErrorVideoProfileCodecNotSupportedKhrException() : base(Result.ErrorVideoProfileCodecNotSupportedKhr){}}