using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTesselation;
using UnityTesselation.Contracts.Generators;
using UnityTesselation.Defaults;

public class CaveMeshTransform : MonoBehaviour, IMeshTransform<CaveVertex> 
{
    private List<int> indices = new List<int>();
    [SerializeField]
    private MeshFilter meshFilter = null;
    private List<Vector3> vertices = new List<Vector3>();
    private List<Vector2> uvs = new List<Vector2>();

    public void Consume(IEnumerable<CaveVertex> items)
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

            uvs.Add(quad[0].UVCords);
            uvs.Add(quad[1].UVCords);
            uvs.Add(quad[2].UVCords);
            uvs.Add(quad[3].UVCords);
        }
    }

    public void Finish()
    {
        var mesh = meshFilter.mesh;
        mesh.SetVertices(vertices);
        mesh.SetTriangles(indices, 0);
        mesh.SetUVs(0, uvs);
        mesh.UploadMeshData(true);
        vertices.Clear();
        indices.Clear();
    }
}
