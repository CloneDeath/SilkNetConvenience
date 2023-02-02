using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.RenderPasses; 

public class RenderPassBeginInformation : IGetCreateInfo<RenderPassBeginInfo> {
	public RenderPass RenderPass;
	public Framebuffer Framebuffer;
	public Rect2D RenderArea;
	public ClearValue[] ClearValues = Array.Empty<ClearValue>();
	
	public unsafe ManagedResourceSet<RenderPassBeginInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<RenderPassBeginInfo>(new RenderPassBeginInfo {
			SType = StructureType.RenderPassBeginInfo,
			RenderPass = RenderPass,
			Framebuffer = Framebuffer,
			RenderArea = RenderArea,
			ClearValueCount = (uint)ClearValues.Length,
			PClearValues = resources.AllocateArray(ClearValues)
		}, resources);
	}
}