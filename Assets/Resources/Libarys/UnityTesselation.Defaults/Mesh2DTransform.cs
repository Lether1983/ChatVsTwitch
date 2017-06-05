using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTesselation.Contracts.Generators;

namespace UnityTesselation.Defaults
{
	public abstract class Mesh2DTransform<TVertex> : MonoBehaviour, IMeshTransform<TVertex> where TVertex : Vertex
	{
		private List<int> indices = new List<int>();
		[SerializeField]
		private MeshFilter meshFilter = null;
		private List<Vector3> vertices = new List<Vector3>();

		public void Consume(IEnumerable<TVertex> items)
		{
			var quads = items.Split(4).Select(x => x.ToArray());
			foreach (var quad in quads)
			{
				vertices.Add(quad[0].Location);
				vertices.Add(quad[1].Location);
				vertices.Add(quad[2].Location);
				vertices.Add(quad[3].Location);

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
			var mesh = meshFilter.mesh;
			mesh.SetVertices(vertices);
			mesh.SetTriangles(indices, 0);
			mesh.UploadMeshData(true);
			vertices.Clear();
			indices.Clear();
		}
	}
}
