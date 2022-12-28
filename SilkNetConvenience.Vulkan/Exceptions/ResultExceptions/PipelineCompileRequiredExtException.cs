using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class PipelineCompileRequiredExtException : ResultFailureException { public PipelineCompileRequiredExtException() : base(Result.PipelineCompileRequiredExt){}}