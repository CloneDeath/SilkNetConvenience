using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorVideoStdVersionNotSupportedKhrException : VulkanResultException { public ErrorVideoStdVersionNotSupportedKhrException() : base(Result.ErrorVideoStdVersionNotSupportedKhr){}}