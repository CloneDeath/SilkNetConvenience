using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorIncompatibleDisplayKhrException : VulkanResultException { public ErrorIncompatibleDisplayKhrException() : base(Result.ErrorIncompatibleDisplayKhr){}}