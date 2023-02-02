using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ThreadDoneKhrException : VulkanResultException { public ThreadDoneKhrException() : base(Result.ThreadDoneKhr){}}