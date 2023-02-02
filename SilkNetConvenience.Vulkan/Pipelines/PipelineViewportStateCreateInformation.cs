using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Pipelines;

public class PipelineViewportStateCreateInformation : IGetCreateInfo<PipelineViewportStateCreateInfo> {
	public Rect2D[] Scissors = Array.Empty<Rect2D>();
	public Viewport[] Viewports = Array.Empty<Viewport>();
	
	public unsafe ManagedResourceSet<PipelineViewportStateCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<PipelineViewportStateCreateInfo>(new PipelineViewportStateCreateInfo {
			SType = StructureType.PipelineViewportStateCreateInfo,
			ScissorCount = (uint)Scissors.Length,
			PScissors = resources.AllocateArray(Scissors),
			ViewportCount = (uint)Viewports.Length,
			PViewports = resources.AllocateArray(Viewports)
		}, resources);
	}
}