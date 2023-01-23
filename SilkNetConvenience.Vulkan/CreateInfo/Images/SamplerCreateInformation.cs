using Silk.NET.Core;
using Silk.NET.Vulkan;

namespace SilkNetConvenience.CreateInfo.Images; 

public class SamplerCreateInformation {
	public SamplerCreateFlags Flags;
	public Bool32 AnisotropyEnable;
	public BorderColor BorderColor;
	public Bool32 CompareEnable;
	public CompareOp CompareOp;
	public Filter MagFilter;
	public float MaxAnisotropy;
	public float MaxLod;
	public Filter MinFilter;
	public float MinLod;
	public SamplerMipmapMode MipmapMode;
	public Bool32 UnnormalizedCoordinates;
	public SamplerAddressMode AddressModeU;
	public SamplerAddressMode AddressModeV;
	public SamplerAddressMode AddressModeW;
	public float MipLodBias;

	public SamplerCreateInfo GetCreateInfo() {
		return new SamplerCreateInfo {
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
		};
	}
}