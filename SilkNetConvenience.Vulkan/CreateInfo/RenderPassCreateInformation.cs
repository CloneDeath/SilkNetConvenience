using System;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo; 

public class RenderPassCreateInformation {
	public RenderPassCreateFlags Flags;
	public AttachmentDescription[] Attachments = Array.Empty<AttachmentDescription>();
	public SubpassDescription[] Subpasses = Array.Empty<SubpassDescription>();
	public SubpassDependency[] Dependencies = Array.Empty<SubpassDependency>();

	public unsafe RenderPassCreateInfo GetCreateInfo() {
		fixed (AttachmentDescription* attachmentsPtr = Attachments)
		fixed (SubpassDescription* subpassesPtr = Subpasses)
		fixed (SubpassDependency* dependenciesPtr = Dependencies) {
			return new RenderPassCreateInfo {
				SType = StructureType.RenderPassCreateInfo,
				Flags = Flags,
				AttachmentCount = (uint)Attachments.Length,
				PAttachments = attachmentsPtr,
				SubpassCount = (uint)Subpasses.Length,
				PSubpasses = subpassesPtr,
				DependencyCount = (uint)Dependencies.Length,
				PDependencies = dependenciesPtr
			};
		}
	}
}