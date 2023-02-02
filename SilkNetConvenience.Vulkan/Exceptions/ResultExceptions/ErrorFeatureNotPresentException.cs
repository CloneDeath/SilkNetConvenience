using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorFeatureNotPresentException : VulkanResultException { public ErrorFeatureNotPresentException() : base(Result.ErrorFeatureNotPresent){}}