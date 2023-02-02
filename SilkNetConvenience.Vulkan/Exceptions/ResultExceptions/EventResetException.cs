using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class EventResetException : VulkanResultException { public EventResetException() : base(Result.EventReset){}}