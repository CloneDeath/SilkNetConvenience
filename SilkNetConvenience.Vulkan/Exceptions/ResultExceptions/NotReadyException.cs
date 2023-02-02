using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class NotReadyException : VulkanResultException { public NotReadyException() : base(Result.NotReady){}}