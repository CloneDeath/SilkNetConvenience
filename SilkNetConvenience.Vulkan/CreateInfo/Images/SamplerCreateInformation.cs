using Silk.NET.Core;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Images; 

public class SamplerCreateInformation : IGetCreateInfo<SamplerCreateInfo> {
	public SamplerCreateFlags Flags;
	public Bool32 AnisotropyEnable;
	public BorderColor BorderColor;
	public bool CompareEnable;
	public CompareOp CompareOp;
	public Filter MagFilter;
	public float MaxAnisotropy;
	public float MaxLod;
	public Filter MinFilter;
	public float MinLod;
	public SamplerMipmapMode MipmapMode;
	public bool UnnormalizedCoordinates;
	public SamplerAddressMode AddressModeU;
	public SamplerAddressMode AddressModeV;
	public SamplerAddressMode AddressModeW;
	public float MipLodBias;

	public ManagedResourceSet<SamplerCreateInfo> GetCreateInfo() {
		var resources = new ManagedResources();
		return new ManagedResourceSet<SamplerCreateInfo>(new SamplerCreateInfo {
			SType = StructureType.SamplerCreateInfo,
			Flags = Flags,
			AnisotropyEnable = AnisotropyEnable,
			BorderColor = BorderColor,
			CompareEnable = CompareEnable,
			CompareOp = CompareOp,
			MagFilter = MagFilter,
			MaxAnisotropy = MaxAnisotropy,
			MaxLod = MaxLod,
			MinFilter = MinFilter,
			MinLod = MinLod,
			MipmapMode = MipmapMode,
			UnnormalizedCoordinates = UnnormalizedCoordinates,
			AddressModeU = AddressModeU,
			AddressModeV = AddressModeV,
			AddressModeW = AddressModeW,
			MipLodBias = MipLodBias
		}, resources);
	}
}