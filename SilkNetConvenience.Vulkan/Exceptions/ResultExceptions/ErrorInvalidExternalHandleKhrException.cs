using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorInvalidExternalHandleKhrException : VulkanResultException { public ErrorInvalidExternalHandleKhrException() : base(Result.ErrorInvalidExternalHandleKhr){}}