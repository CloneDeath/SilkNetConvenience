using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorSurfaceLostKhrException : VulkanResultException { public ErrorSurfaceLostKhrException() : base(Result.ErrorSurfaceLostKhr){}}