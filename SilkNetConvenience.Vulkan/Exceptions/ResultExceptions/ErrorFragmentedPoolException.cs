using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ErrorFragmentedPoolException : ResultFailureException { public ErrorFragmentedPoolException() : base(Result.ErrorFragmentedPool){}}