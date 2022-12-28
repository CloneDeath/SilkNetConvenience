using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class NotReadyException : ResultFailureException { public NotReadyException() : base(Result.NotReady){}}