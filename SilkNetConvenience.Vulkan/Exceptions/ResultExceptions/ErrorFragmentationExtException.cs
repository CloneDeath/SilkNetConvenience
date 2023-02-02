using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorFragmentationExtException : VulkanResultException { public ErrorFragmentationExtException() : base(Result.ErrorFragmentationExt){}}