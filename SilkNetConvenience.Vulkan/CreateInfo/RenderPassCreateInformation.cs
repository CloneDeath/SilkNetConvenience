using System;
using System.Linq;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo.Pipelines;

namespace SilkNetConvenience.CreateInfo; 

public class RenderPassCreateInformation {
	public RenderPassCreateFlags Flags;
	public AttachmentDescription[] Attachments = Array.Empty<AttachmentDescription>();
	public SubpassDescriptionInformation[] Subpasses = Array.Empty<SubpassDescriptionInformation>();
	public SubpassDependency[] Dependencies = Array.Empty<SubpassDependency>();

	public unsafe RenderPassCreateInfo GetCreateInfo() {
		var subpasses = Subpasses.Select(s => s.GetSubpassDescription()).ToArray();
		fixed (AttachmentDescription* attachmentsPtr = Attachments)
		fixed (SubpassDescription* subpassesPtr = subpasses)
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