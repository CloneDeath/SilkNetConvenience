using System.Collections;
using Silk.NET.Assimp;

namespace SilkNetConvenience.Assimp.Wrappers; 

public unsafe class AssimpNode {
	private readonly Scene* _scene;
	private readonly Node* _node;

	public AssimpNode(Scene* scene, Node* node) {
		_scene = scene;
		_node = node;
	}

	public IEnumerable<AssimpMesh> Meshes {
		get {
			var meshes = new List<AssimpMesh>();
			for (var m = 0; m < _node->MNumMeshes; m++) {
				var mesh = _scene->MMeshes[_node->MMeshes[m]];
				meshes.Add(new AssimpMesh(mesh));
			}
			return meshes;
		}
	}

	public IEnumerable<AssimpNode> Children {
		get {
			var children = new List<AssimpNode>();
			for (var i = 0; i < _node->MNumChildren; i++) {
				var child = _node->MChildren[i];
				children.Add(new AssimpNode(_scene, child));
			}
			return children;
		}
	}
}