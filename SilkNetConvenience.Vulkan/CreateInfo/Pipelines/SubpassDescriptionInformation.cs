using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Pipelines; 

public class SubpassDescriptionInformation {
	public SubpassDescriptionFlags Flags = default;
	public AttachmentReference[] ColorAttachments = Array.Empty<AttachmentReference>();
	public AttachmentReference[] InputAttachments = Array.Empty<AttachmentReference>();
	public PipelineBindPoint PipelineBindPoint = default;
	public uint[] PreserveAttachments = Array.Empty<uint>();
	public AttachmentReference[] ResolveAttachments = Array.Empty<AttachmentReference>();
	public AttachmentReference DepthStencilAttachment = default;
	
	public unsafe SubpassDescription GetSubpassDescription() {
		if (ResolveAttachments.Length > 0 && ResolveAttachments.Length != ColorAttachments.Length) {
			throw new Exception("Number of ResolveAttachments must be either empty, or match the number of ColorAttachments");
		}
		fixed (AttachmentReference* colorAttachmentsRef = ColorAttachments)
		fixed (AttachmentReference* inputAttachmentsRef = InputAttachments)
		fixed (uint* preserveAttachmentsRef = PreserveAttachments)
		fixed (AttachmentReference* resolveAttachmentsRef = ResolveAttachments)
		fixed (AttachmentReference* depthStencilRef = new[]{DepthStencilAttachment})
		{
			return new SubpassDescription {
				Flags = Flags,
				ColorAttachmentCount = (uint)ColorAttachments.Length,
				PColorAttachments = colorAttachmentsRef,
				InputAttachmentCount = (uint)InputAttachments.Length,
				PInputAttachments = inputAttachmentsRef,
				PipelineBindPoint = PipelineBindPoint,
				PreserveAttachmentCount = (uint)PreserveAttachments.Length,
				PPreserveAttachments = preserveAttachmentsRef,
				PResolveAttachments = resolveAttachmentsRef,
				PDepthStencilAttachment = depthStencilRef
			};
		}
	}
}