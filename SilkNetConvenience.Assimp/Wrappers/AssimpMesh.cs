using System.Numerics;
using Silk.NET.Assimp;

namespace SilkNetConvenience.Assimp.Wrappers; 

public unsafe class AssimpMesh {
	private readonly Mesh* _mesh;
	public readonly Vector3[] Vertices;
	public readonly AssimpFace[] Faces;

	public AssimpMesh(Mesh* mesh) {
		_mesh = mesh;
		Vertices = new Vector3[mesh->MNumVertices];
		for (var i = 0; i < Vertices.Length; i++) {
			Vertices[i] = mesh->MVertices[i];
		}
		
		Faces = new AssimpFace[mesh->MNumFaces];
		for (var f = 0; f < Faces.Length; f++) {
			Faces[f] = new AssimpFace(mesh->MFaces[f]);
		}
	}

	public Mesh.MTextureCoordsBuffer TextureCoords => _mesh->MTextureCoords;
}