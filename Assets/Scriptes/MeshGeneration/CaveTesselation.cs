using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTesselation;
using UnityTesselation.Contracts.Generators;

public class CaveTesselation : MonoBehaviour, ITesselation<Vector2, int>, IEdgeGenerator<CaveNode, Vector2, int>, IVertexGenerator<CaveVertex, CaveNode, Vector2, int>
{
    private static readonly Vector2[] surrounds = new[]
    {
            Vector2.left,
            Vector2.up,
            Vector2.right,
            Vector2.down
        };
    private static readonly Vector2[] vertices = new[]
    {
            Vector2.zero,
            Vector2.up,
            Vector2.one,
            Vector2.right
        };
    [SerializeField]
    private GameManager gManager = null;
    [SerializeField]
    TileManager tManager;

    public GameManager GameManager { get { return gManager; } }

    IEnumerable<Edge<Vector2>> IGenerator<Edge<Vector2>, CaveNode, Vector2, int>.Generate(CaveNode node)
    {
        Func<Edge<Vector2>> left = () => new Edge<Vector2>(node.Point, node.Point + Vector2.up);
        Func<Edge<Vector2>> right = () => new Edge<Vector2>(node.Point + Vector2.one, node.Point + Vector2.right);
        Func<Edge<Vector2>> top = () => new Edge<Vector2>(node.Point + Vector2.up, node.Point + Vector2.one);
        Func<Edge<Vector2>> bottom = () => new Edge<Vector2>(node.Point + Vector2.right, node.Point);

        if ((node.Case & 1) > 0)
            yield return bottom();
        if ((node.Case & 2) > 0)
            yield return top();
        if ((node.Case & 4) > 0)
            yield return right();
        if ((node.Case & 8) > 0)
            yield return left();
    }

    IEnumerable<CaveVertex> IGenerator<CaveVertex, CaveNode, Vector2, int>.Generate(CaveNode node)
    {
        /*
        *  0 = Ground
        *  1 = Wall 
        *  2 = Room
        *  3 = Way
        *  4 = Door
        *  5 = EnemyGuy
        *  6 = Mine
        *  7 = Target
        *  8 = Bush
        *  9 = Tree
        * 10 = Stone
        * 11 = Trap
        */

        /*Zahlen für Entities
         * 1024,"door",
         * 2048,"EnemyGuy",
         * 4096,"Mine",
         * 8192,"Target",
         * 16384,"Bush",
         * 32768,"Tree",
         * 65536,"Stone",
         * 131072,"Trap",
         * 262144,"Spawnpoint",
         * 524288,"Exitpoint"
         * 
         */

        var tiles = default(Dictionary<int, string>);
        if (node.Self.Key == 1)
        {
            tiles = new Dictionary<int, string>()
            {
                { 0, "erde2" },
                { 1, "beton_indoor" },
                { 2, "pflaster" },
            };
        }
        else if (node.Self.Key == 2)
        {
            tiles = new Dictionary<int, string>()
            {
                { 0, "blacktile" },
            };
        }
        Debug.Log(node.Metadata);
        // string[] tiles = new[] { "erde2", "blacktile", "beton_indoor", "pflaster", "wall", "kacheln", "beton_outdoor", "gras", "asphalt", "erde2", "rock_placeholder", "trap_placeholder" };
        var uv = tManager.getUV(tiles[node.Metadata & 127]);

        for (int i = 0; i < 4; i++)
        {
            yield return new CaveVertex()
            {
                Location = vertices[i] + node.Point,
                UVCords = uv[i]
            };
        }

    }

    int ITesselation<Vector2, int>.Key(Vector2 point)
    {
        return gManager.levelMap.Get(point) & 3;
    }

    bool IEdgeGenerator<CaveNode, Vector2, int>.OuterNode(int set)
    {
        return set > 0;
    }

    IEnumerable<Vector2> ITesselation<Vector2, int>.Points()
    {
        return Enumerable.Range(0, gManager.levelMap.MapWidth * gManager.levelMap.MapHeight).Select(i => new Vector2(i % gManager.levelMap.MapWidth, i / gManager.levelMap.MapWidth % gManager.levelMap.MapHeight));
    }

    IEnumerable<Vector2> ITesselation<Vector2, int>.Surround(Vector2 v)
    {
        for (int i = 0; i < 4; i++)
        {
            yield return v + surrounds[i];
        }
    }

    bool ITesselation<Vector2, int>.Tesselate(int index)
    {
        return true;
    }

    bool ITesselation<Vector2, int>.Valid(Vector2 point)
    {
        return !(point.x < 0 || point.y < 0 || point.x >= gManager.levelMap.MapWidth || point.y >= gManager.levelMap.MapHeight);
    }
}
