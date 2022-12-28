using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class EventResetException : ResultFailureException { public EventResetException() : base(Result.EventReset){}}