using Silk.NET.Assimp;

namespace SilkNetConvenience.Assimp.Wrappers; 

public unsafe class AssimpScene : BaseAssimpWrapper {
	public readonly Silk.NET.Assimp.Assimp Assimp;
	public readonly Scene* Scene;

	public AssimpScene(Silk.NET.Assimp.Assimp assimp, string modelPath, uint flags) {
		Assimp = assimp;
		Scene = assimp.ImportFile(modelPath, flags);
	}

	public AssimpNode RootNode => new(Scene, Scene->MRootNode);

	protected override void ReleaseAssimpResources() {
		Assimp.ReleaseImport(Scene);
	}
}