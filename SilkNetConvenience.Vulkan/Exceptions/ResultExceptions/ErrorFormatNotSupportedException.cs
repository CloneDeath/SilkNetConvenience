using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorFormatNotSupportedException : VulkanResultException { public ErrorFormatNotSupportedException() : base(Result.ErrorFormatNotSupported){}}