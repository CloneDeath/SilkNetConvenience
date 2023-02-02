using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorDeviceLostException : VulkanResultException { public ErrorDeviceLostException() : base(Result.ErrorDeviceLost){}}