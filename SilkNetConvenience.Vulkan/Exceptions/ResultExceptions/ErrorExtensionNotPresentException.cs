using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorExtensionNotPresentException : VulkanResultException { public ErrorExtensionNotPresentException() : base(Result.ErrorExtensionNotPresent){}}