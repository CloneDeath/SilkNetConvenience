using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo;

public class SubmitInformation {
	public Semaphore[] SignalSemaphores = Array.Empty<Semaphore>();
	public Semaphore[] WaitSemaphores = Array.Empty<Semaphore>();
	public CommandBuffer[] CommandBuffers = Array.Empty<CommandBuffer>();
	public PipelineStageFlags[] PipelineStageFlags = Array.Empty<PipelineStageFlags>();
}