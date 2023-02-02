using Silk.NET.Vulkan;

namespace SilkNetConvenience.Exceptions.ResultExceptions;

public class PipelineCompileRequiredExtException : VulkanResultException { public PipelineCompileRequiredExtException() : base(Result.PipelineCompileRequiredExt){}}