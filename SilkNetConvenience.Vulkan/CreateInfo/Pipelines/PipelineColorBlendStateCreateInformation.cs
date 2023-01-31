using System;
using Silk.NET.Maths;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Pipelines;

public class PipelineColorBlendStateCreateInformation : IGetCreateInfo<PipelineColorBlendStateCreateInfo> {
	public PipelineColorBlendStateCreateFlags Flags;
	public PipelineColorBlendAttachmentState[] Attachments = Array.Empty<PipelineColorBlendAttachmentState>();
	public Vector4D<float> BlendConstants;
	public LogicOp LogicOp;
	public bool LogicOpEnable;

	public unsafe ManagedResourceSet<PipelineColorBlendStateCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		var createInfo = new PipelineColorBlendStateCreateInfo {
			SType = StructureType.PipelineColorBlendStateCreateInfo,
			Flags = Flags,
			AttachmentCount = (uint)Attachments.Length,
			PAttachments = resources.AllocateArray(Attachments),
			LogicOp = LogicOp,
			LogicOpEnable = LogicOpEnable
		};
		createInfo.BlendConstants[0] = BlendConstants[0];
		createInfo.BlendConstants[1] = BlendConstants[1];
		createInfo.BlendConstants[2] = BlendConstants[2];
		createInfo.BlendConstants[3] = BlendConstants[3];
		return new ManagedResourceSet<PipelineColorBlendStateCreateInfo>(createInfo, resources);
	}
}