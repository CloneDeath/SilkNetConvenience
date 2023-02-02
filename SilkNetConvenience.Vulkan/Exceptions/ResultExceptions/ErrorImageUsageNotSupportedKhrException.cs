using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorImageUsageNotSupportedKhrException : VulkanResultException { public ErrorImageUsageNotSupportedKhrException() : base(Result.ErrorImageUsageNotSupportedKhr){}}