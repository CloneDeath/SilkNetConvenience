using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo; 

public class RenderPassBeginInformation {
	public RenderPass RenderPass;
	public Framebuffer Framebuffer;
	public Rect2D RenderArea;
	public ClearValue[] ClearValues = Array.Empty<ClearValue>();
	
	public unsafe RenderPassBeginInfo GetBeginInfo() {
		fixed (ClearValue* cvp = ClearValues) {
			return new RenderPassBeginInfo {
				SType = StructureType.RenderPassBeginInfo,
				RenderPass = RenderPass,
				Framebuffer = Framebuffer,
				RenderArea = RenderArea,
				ClearValueCount = (uint)ClearValues.Length,
				PClearValues = cvp
			};
		}
	}
}