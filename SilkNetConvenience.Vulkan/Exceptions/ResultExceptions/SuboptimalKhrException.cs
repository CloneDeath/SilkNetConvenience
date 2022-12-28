using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class SuboptimalKhrException : ResultFailureException { public SuboptimalKhrException() : base(Result.SuboptimalKhr){}}