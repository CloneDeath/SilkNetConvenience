using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class EventSetException : ResultFailureException { public EventSetException() : base(Result.EventSet){}}