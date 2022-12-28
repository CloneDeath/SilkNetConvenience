using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class OperationNotDeferredKhrException : ResultFailureException { public OperationNotDeferredKhrException() : base(Result.OperationNotDeferredKhr){}}