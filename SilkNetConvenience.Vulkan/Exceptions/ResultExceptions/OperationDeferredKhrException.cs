using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class OperationDeferredKhrException : VulkanResultException { public OperationDeferredKhrException() : base(Result.OperationDeferredKhr){}}