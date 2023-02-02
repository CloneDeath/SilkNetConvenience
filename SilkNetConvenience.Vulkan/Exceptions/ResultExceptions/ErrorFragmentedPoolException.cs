using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorFragmentedPoolException : VulkanResultException { public ErrorFragmentedPoolException() : base(Result.ErrorFragmentedPool){}}