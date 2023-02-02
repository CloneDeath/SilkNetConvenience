using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.Buffers; 

public class FramebufferCreateInformation : IGetCreateInfo<FramebufferCreateInfo> {
	public Silk.NET.Vulkan.RenderPass RenderPass;
	public FramebufferCreateFlags Flags;
	public uint Height;
	public uint Layers;
	public uint Width;
	public ImageView[] Attachments = Array.Empty<ImageView>(); 

	public unsafe ManagedResourceSet<FramebufferCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<FramebufferCreateInfo>(new FramebufferCreateInfo {
			SType = StructureType.FramebufferCreateInfo,
			RenderPass = RenderPass,
			Flags = Flags,
			Height = Height,
			Layers = Layers,
			Width = Width,
			AttachmentCount = (uint)Attachments.Length,
			PAttachments = resources.AllocateArray(Attachments)
		}, resources);
	}
}