using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ThreadIdleKhrException : VulkanResultException { public ThreadIdleKhrException() : base(Result.ThreadIdleKhr){}}