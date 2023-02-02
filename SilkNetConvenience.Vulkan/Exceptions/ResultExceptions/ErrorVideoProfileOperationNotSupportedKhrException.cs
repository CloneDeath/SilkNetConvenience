using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorVideoProfileOperationNotSupportedKhrException : VulkanResultException { public ErrorVideoProfileOperationNotSupportedKhrException() : base(Result.ErrorVideoProfileOperationNotSupportedKhr){}}