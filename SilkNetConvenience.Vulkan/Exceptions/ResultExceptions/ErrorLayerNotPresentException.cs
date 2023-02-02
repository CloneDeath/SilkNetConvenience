using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorLayerNotPresentException : VulkanResultException { public ErrorLayerNotPresentException() : base(Result.ErrorLayerNotPresent){}}