using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTesselation;
using UnityTesselation.Contracts.Factories;
using UnityTesselation.Contracts.Generators;
using UnityTesselation.Defaults;

public class CaveTesselator : UnityTesselator2D<CaveVertex, CaveNode, int>, IMeshTransformProvider<CaveVertex, int>, IColliderTransformProvider<Edge<Vector2>, int>, INodeFactory<CaveNode, Vector2, int>
{
    [SerializeField]
    private CaveColliderTransform colliderPrefab = null;
    private Dictionary<int, IColliderTransform<Edge<Vector2>>> colliders = new Dictionary<int, IColliderTransform<Edge<Vector2>>>();
    private Dictionary<int, IMeshTransform<CaveVertex>> meshes = new Dictionary<int, IMeshTransform<CaveVertex>>();
    [SerializeField]
    private CaveMeshTransform meshPrefab = null;
    [SerializeField]
    private CaveTesselation tessalation = null;

    protected override IColliderTransformProvider<Edge<Vector2>, int> ColliderTransformProvider
    {
        get
        {
            return this;
        }
    }

    protected override ICollisionGenerator<Edge<Vector2>, CaveNode, Vector2, int> CollisionGenerator
    {
        get
        {
            return tessalation;
        }
    }

    protected override IMeshTransformProvider<CaveVertex, int> MeshTransformProvider
    {
        get
        {
            return this;
        }
    }

    protected override INodeFactory<CaveNode, Vector2, int> NodeFactory
    {
        get
        {
            return this;
        }
    }

    protected override ITesselation<Vector2, int> Tesselation
    {
        get
        {
            return tessalation;
        }
    }

    protected override IVertexGenerator<CaveVertex, CaveNode, Vector2, int> VertexGenerator
    {
        get
        {
            return tessalation;
        }
    }

    public CaveNode Create(Area<int> self, Vector2 point)
    {
        return new CaveNode(self, point, tessalation.GameManager.levelMap.Get(point) >> 3);
    }

    IColliderTransform<Edge<Vector2>> IColliderTransformProvider<Edge<Vector2>, int>.Get(Area<int> area)
    {
        if (area.Key == 2)
        {
            var collider = default(IColliderTransform<Edge<Vector2>>);
            if(!colliders.TryGetValue(area.Key,out collider))
            {
                collider = colliders[area.Key] = Instantiate(colliderPrefab, transform, false);
            }
            return collider;
        }
        else
        {
            return new DisabledColliderTransform<Edge<Vector2>>();
        }
    }

    IMeshTransform<CaveVertex> IMeshTransformProvider<CaveVertex, int>.Get(Area<int> area)
    {
        var mesh = default(IMeshTransform<CaveVertex>);
        if(!meshes.TryGetValue(area.Key,out mesh))
        {
            mesh = meshes[area.Key] = Instantiate(meshPrefab, transform, false);
        }
        return mesh;
    }
}
