using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorVideoProfileCodecNotSupportedKhrException : VulkanResultException { public ErrorVideoProfileCodecNotSupportedKhrException() : base(Result.ErrorVideoProfileCodecNotSupportedKhr){}}