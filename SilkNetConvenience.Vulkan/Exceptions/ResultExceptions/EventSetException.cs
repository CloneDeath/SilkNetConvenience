using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class EventSetException : VulkanResultException { public EventSetException() : base(Result.EventSet){}}