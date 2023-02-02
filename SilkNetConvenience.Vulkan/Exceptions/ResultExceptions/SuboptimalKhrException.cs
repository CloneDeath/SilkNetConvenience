using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class SuboptimalKhrException : VulkanResultException { public SuboptimalKhrException() : base(Result.SuboptimalKhr){}}