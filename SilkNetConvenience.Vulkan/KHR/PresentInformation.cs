using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.KHR; 

public class PresentInformation : IGetCreateInfo<PresentInfoKHR> {
	public SwapchainKHR[] Swapchains = Array.Empty<SwapchainKHR>();
	public Semaphore[] WaitSemaphores = Array.Empty<Semaphore>();
	public uint[] ImageIndices = Array.Empty<uint>();
	
	public unsafe ManagedResourceSet<PresentInfoKHR> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<PresentInfoKHR>(new PresentInfoKHR {
			SType = StructureType.PresentInfoKhr,
			SwapchainCount = (uint)Swapchains.Length,
			PSwapchains = resources.AllocateArray(Swapchains),
			WaitSemaphoreCount = (uint)WaitSemaphores.Length,
			PWaitSemaphores = resources.AllocateArray(WaitSemaphores),
			PImageIndices = resources.AllocateArray(ImageIndices)
		}, resources);
	}
}