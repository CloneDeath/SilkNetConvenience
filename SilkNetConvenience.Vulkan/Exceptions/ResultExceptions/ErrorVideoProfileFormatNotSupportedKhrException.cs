using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorVideoProfileFormatNotSupportedKhrException : VulkanResultException { public ErrorVideoProfileFormatNotSupportedKhrException() : base(Result.ErrorVideoProfileFormatNotSupportedKhr){}}