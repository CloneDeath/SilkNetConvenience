using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class TimeoutException : VulkanResultException { public TimeoutException() : base(Result.Timeout){}}