using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo; 

public class FramebufferCreateInformation {
	public RenderPass RenderPass;
	public FramebufferCreateFlags Flags;
	public uint Height;
	public uint Layers;
	public uint Width;
	public ImageView[] Attachments = Array.Empty<ImageView>(); 

	public unsafe FramebufferCreateInfo GetCreateInfo() {
		fixed (ImageView* attachmentsRef = Attachments) {
			return new FramebufferCreateInfo {
				SType = StructureType.FramebufferCreateInfo,
				RenderPass = RenderPass,
				Flags = Flags,
				Height = Height,
				Layers = Layers,
				Width = Width,
				AttachmentCount = (uint)Attachments.Length,
				PAttachments = attachmentsRef
			};
		}
	}
}