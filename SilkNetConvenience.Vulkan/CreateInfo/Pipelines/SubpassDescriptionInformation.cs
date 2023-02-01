using System;
using System.Linq;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Pipelines; 

public class SubpassDescriptionInformation : IGetCreateInfo<SubpassDescription> {
	public SubpassDescriptionFlags Flags = default;
	public AttachmentReference[] ColorAttachments = Array.Empty<AttachmentReference>();
	public AttachmentReference[] InputAttachments = Array.Empty<AttachmentReference>();
	public PipelineBindPoint PipelineBindPoint = default;
	public uint[] PreserveAttachments = Array.Empty<uint>();
	public AttachmentReference[] ResolveAttachments = Array.Empty<AttachmentReference>();
	public AttachmentReference? DepthStencilAttachment;

	public unsafe ManagedResourceSet<SubpassDescription> GetCreateInfo() {
		if (ResolveAttachments.Length > 0 && ResolveAttachments.Length != ColorAttachments.Length) {
			throw new Exception("Number of ResolveAttachments must be either empty, or match the number of ColorAttachments");
		}
		var resources = new ManagedResources();
		return new ManagedResourceSet<SubpassDescription>(new SubpassDescription {
			Flags = Flags,
			ColorAttachmentCount = (uint)ColorAttachments.Length,
			PColorAttachments = resources.AllocateArray(ColorAttachments),
			InputAttachmentCount = (uint)InputAttachments.Length,
			PInputAttachments = resources.AllocateArray(InputAttachments),
			PipelineBindPoint = PipelineBindPoint,
			PreserveAttachmentCount = (uint)PreserveAttachments.Length,
			PPreserveAttachments = resources.AllocateArray(PreserveAttachments),
			PResolveAttachments = ResolveAttachments.Any() ? resources.AllocateArray(ResolveAttachments) : null,
			PDepthStencilAttachment = DepthStencilAttachment.HasValue ? resources.AllocateStruct(DepthStencilAttachment.Value) : null
		}, resources);
	}
}