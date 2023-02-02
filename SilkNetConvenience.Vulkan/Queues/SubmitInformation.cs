using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Queues;

public class SubmitInformation : IGetCreateInfo<SubmitInfo> {
	public Semaphore[] SignalSemaphores = Array.Empty<Semaphore>();
	public Semaphore[] WaitSemaphores = Array.Empty<Semaphore>();
	public CommandBuffer[] CommandBuffers = Array.Empty<CommandBuffer>();
	public PipelineStageFlags[] WaitDstStageMask = Array.Empty<PipelineStageFlags>();

	public unsafe ManagedResourceSet<SubmitInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<SubmitInfo>(new SubmitInfo {
			SType = StructureType.SubmitInfo,
			CommandBufferCount = (uint)CommandBuffers.Length,
			PCommandBuffers = resources.AllocateArray(CommandBuffers),
			SignalSemaphoreCount = (uint)SignalSemaphores.Length,
			PSignalSemaphores = resources.AllocateArray(SignalSemaphores),
			WaitSemaphoreCount = (uint)WaitSemaphores.Length,
			PWaitSemaphores = resources.AllocateArray(WaitSemaphores),
			PWaitDstStageMask = resources.AllocateArray(WaitDstStageMask)
		}, resources);
	}
}