namespace SilkNetConvenience.Assimp.Wrappers; 

public class AssimpContext : IDisposable {
	public readonly Silk.NET.Assimp.Assimp Assimp = Silk.NET.Assimp.Assimp.GetApi();

	public void Dispose() {
		Assimp.Dispose();
		GC.SuppressFinalize(this);
	}

	public AssimpScene ImportFile(string modelPath, uint flags) {
		return new AssimpScene(Assimp, modelPath, flags);
	}
}