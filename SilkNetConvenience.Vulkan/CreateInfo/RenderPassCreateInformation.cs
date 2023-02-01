using System;
using Silk.NET.Vulkan;
using SilkNetConvenience.CreateInfo.Pipelines;

namespace SilkNetConvenience.CreateInfo; 

public class RenderPassCreateInformation : IGetCreateInfo<RenderPassCreateInfo> {
	public RenderPassCreateFlags Flags;
	public AttachmentDescription[] Attachments = Array.Empty<AttachmentDescription>();
	public SubpassDescriptionInformation[] Subpasses = Array.Empty<SubpassDescriptionInformation>();
	public SubpassDependency[] Dependencies = Array.Empty<SubpassDependency>();

	public unsafe ManagedResourceSet<RenderPassCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<RenderPassCreateInfo>(new RenderPassCreateInfo {
			SType = StructureType.RenderPassCreateInfo,
			Flags = Flags,
			AttachmentCount = (uint)Attachments.Length,
			PAttachments = resources.AllocateArray(Attachments),
			SubpassCount = (uint)Subpasses.Length,
			PSubpasses = resources.AllocateCreateInfos<SubpassDescription, SubpassDescriptionInformation>(Subpasses),
			DependencyCount = (uint)Dependencies.Length,
			PDependencies = resources.AllocateArray(Dependencies)
		}, resources);
	}
}