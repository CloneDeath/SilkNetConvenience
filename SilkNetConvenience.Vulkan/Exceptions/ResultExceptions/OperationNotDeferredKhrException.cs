using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class OperationNotDeferredKhrException : VulkanResultException { public OperationNotDeferredKhrException() : base(Result.OperationNotDeferredKhr){}}