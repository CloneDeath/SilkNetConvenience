using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorNotPermittedExtException : VulkanResultException { public ErrorNotPermittedExtException() : base(Result.ErrorNotPermittedExt){}}