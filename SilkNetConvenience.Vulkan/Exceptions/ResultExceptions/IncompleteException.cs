using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class IncompleteException : VulkanResultException { public IncompleteException() : base(Result.Incomplete){}}