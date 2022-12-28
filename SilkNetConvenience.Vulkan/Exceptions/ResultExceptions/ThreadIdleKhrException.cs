using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ThreadIdleKhrException : ResultFailureException { public ThreadIdleKhrException() : base(Result.ThreadIdleKhr){}}