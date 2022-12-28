using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class OperationDeferredKhrException : ResultFailureException { public OperationDeferredKhrException() : base(Result.OperationDeferredKhr){}}