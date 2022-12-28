using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class ThreadDoneKhrException : ResultFailureException { public ThreadDoneKhrException() : base(Result.ThreadDoneKhr){}}