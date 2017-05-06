using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField]
    TileManager tManager;
    [SerializeField]
    MeshFilter filter;
    Mesh mesh;
    private void Awake()
    {
        filter.sharedMesh = mesh = new Mesh();
    }

    public void CreateMesh(Map map)
    {
        Vector3[] vertices = new Vector3[map.MapWidth * map.MapHeight * 4];
        Vector2[] uvs = new Vector2[map.MapWidth * map.MapHeight * 4];
        int[] triangle = new int[map.MapWidth * map.MapHeight * 6];

        for (int x = 0; x < map.MapWidth; x++)
        {
            for (int y = 0; y < map.MapHeight; y++)
            {
                int tile = map.RandomMap[x, y];
                int Index = y * map.MapWidth + x;
                int vIndex = Index * 4;
                int tIndex = Index * 6;

                Vector3 tilePosition = new Vector3(x, y, 0);

                vertices[vIndex + 0] = tilePosition + Vector3.zero;
                vertices[vIndex + 1] = tilePosition + Vector3.up;
                vertices[vIndex + 2] = tilePosition + new Vector3(1, 1, 0);
                vertices[vIndex + 3] = tilePosition + Vector3.right;

                triangle[tIndex + 0] = vIndex + 0;
                triangle[tIndex + 1] = vIndex + 1;
                triangle[tIndex + 2] = vIndex + 2;
                triangle[tIndex + 3] = vIndex + 2;
                triangle[tIndex + 4] = vIndex + 3;
                triangle[tIndex + 5] = vIndex + 0;

                /*
                 * 0 = Ground
                 * 1 = Wall 
                 * 2 = Room
                 * 3 = Way
                 * 4 = Door
                 * 5 = EnemyGuy
                 * 6 = Mine
                 * 7 = Target
                 */

                string[] tiles = new[] { "erde2", "blacktile", "beton_indoor", "pflaster", "wall", "kacheln", "beton_outdoor", "gras" };
                var uv = tManager.getUV(tiles[tile]);

                uvs[vIndex + 0] = uv[0];
                uvs[vIndex + 1] = uv[1];
                uvs[vIndex + 2] = uv[2];
                uvs[vIndex + 3] = uv[3];
            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangle;
    }
}
