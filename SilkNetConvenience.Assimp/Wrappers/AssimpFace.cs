using Silk.NET.Assimp;

namespace SilkNetConvenience.Assimp.Wrappers;

public unsafe class AssimpFace {
	private readonly Face _face;

	public AssimpFace(Face face) {
		_face = face;
	}

	public IEnumerable<uint> Indices {
		get {
			var indices = new List<uint>();
			for (var i = 0; i < _face.MNumIndices; i++) {
				var index = _face.MIndices[i];
				indices.Add(index);
			}
			return indices;
		}
	}
}