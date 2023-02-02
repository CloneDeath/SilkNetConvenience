using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorValidationFailedExtException : VulkanResultException { public ErrorValidationFailedExtException() : base(Result.ErrorValidationFailedExt){}}