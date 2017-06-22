using System.Collections.Generic;
using UnityEngine;
using UnityTesselation.Contracts.Generators;

namespace UnityTesselation.Defaults
{
	public abstract class Mesh3DTransform<TVertex> : MonoBehaviour, IMeshTransform<TVertex> where TVertex : IVector3Vertex
	{
		private List<int> indices = new List<int>();
		[SerializeField]
		private MeshFilter meshFilter = null;
		private List<TVertex> vertexCache = new List<TVertex>(4);
		private List<Vector3> vertices = new List<Vector3>();

		public void Consume(IEnumerable<TVertex> items)
		{
			vertexCache.Clear();
			foreach (var item in items)
			{
				vertexCache.Add(item);
				if (vertexCache.Count == 4)
				{
					foreach (var vertex in vertexCache)
					{
						vertices.Add(vertex.Location);
					}

					indices.Add(vertices.Count - 4);
					indices.Add(vertices.Count - 3);
					indices.Add(vertices.Count - 2);
					indices.Add(vertices.Count - 2);
					indices.Add(vertices.Count - 1);
					indices.Add(vertices.Count - 4);

					//OnConsume(vertexCache);

					vertexCache.Clear();
				}
			}
		}

		public void Finish()
		{
			var mesh = meshFilter.mesh;
			mesh.SetVertices(vertices);
			mesh.SetTriangles(indices, 0);
			OnFinish(mesh);
			mesh.RecalculateBounds();
			mesh.RecalculateNormals();
			mesh.UploadMeshData(true);
			vertices.Clear();
			indices.Clear();
		}

		protected abstract void OnConsume(TVertex[] block);

		protected abstract void OnFinish(Mesh mesh);
	}
}
