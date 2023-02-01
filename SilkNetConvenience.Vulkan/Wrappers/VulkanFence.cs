using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Wrappers; 

public class VulkanFence : BaseVulkanWrapper {
	public readonly Vk Vk;
	public readonly Device Device;
	public readonly Fence Fence;

	public VulkanFence(VulkanDevice device, FenceCreateFlags flags = FenceCreateFlags.None) : this(device.Vk,
		device.Device, flags) {
		device.AddChildResource(this);
	}
	public VulkanFence(Vk vk, Device device, FenceCreateFlags flags = FenceCreateFlags.None) {
		Vk = vk;
		Device = device;
		Fence = Vk.CreateFence(Device, flags);
	}

	protected override void ReleaseVulkanResources() {
		Vk.DestroyFence(Device, Fence);
	}
	
	public static implicit operator Fence(VulkanFence self) => self.Fence;

	public void Wait(TimeSpan? timeout = null) {
		var timeoutNS = timeout.HasValue ? (ulong)timeout.Value.TotalMilliseconds * 1_000_000 : ulong.MaxValue;
		Vk.WaitForFences(Device, 1, Fence, true, timeoutNS);
	}

	public void Reset() {
		Vk.ResetFences(Device, 1, Fence);
	}
}