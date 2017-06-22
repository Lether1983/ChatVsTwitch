using System.Collections.Generic;
using UnityEngine;
using UnityTesselation.Contracts.Generators;

namespace UnityTesselation.Defaults
{
	public class MeshColliderTransform : MonoBehaviour, IColliderTransform<Face<Vector3>>
	{
		private List<int> indices = new List<int>();
		[SerializeField]
		private MeshCollider meshCollider = null;
		private List<Vector3> vertices = new List<Vector3>();

		public void Consume(IEnumerable<Face<Vector3>> items)
		{
			foreach (var item in items)
			{
				vertices.Add(item.V1);
				vertices.Add(item.V2);
				vertices.Add(item.V3);
				vertices.Add(item.V4);

				indices.Add(vertices.Count - 4);
				indices.Add(vertices.Count - 3);
				indices.Add(vertices.Count - 2);
				indices.Add(vertices.Count - 2);
				indices.Add(vertices.Count - 1);
				indices.Add(vertices.Count - 4);
			}
		}

		public void Finish()
		{
			var mesh = meshCollider.sharedMesh;
			mesh.SetVertices(vertices);
			mesh.SetTriangles(indices, 0);
			mesh.UploadMeshData(true);
			vertices.Clear();
			indices.Clear();
			meshCollider.sharedMesh = mesh;
		}

		private void Awake()
		{
			meshCollider.sharedMesh = new Mesh();
		}
	}
}
