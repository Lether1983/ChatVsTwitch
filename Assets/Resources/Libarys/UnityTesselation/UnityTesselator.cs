using System.Collections.Generic;
using UnityEngine;
using UnityTesselation.Contracts;
using UnityTesselation.Contracts.Factories;
using UnityTesselation.Contracts.Generators;

namespace UnityTesselation
{
	public abstract class UnityTesselator<TVertex, TNode, TPosition, TKey> : MonoBehaviour where TNode : INode<TPosition, TKey>
	{
		protected abstract IColliderTransformProvider<TPosition, TKey> ColliderTransformProvider { get; }

		protected abstract IEdgeGenerator<TNode, TPosition, TKey> EdgeGenerator { get; }

		protected abstract IMeshTransformProvider<TVertex, TKey> MeshTransformProvider { get; }

		protected abstract INodeFactory<TNode, TPosition, TKey> NodeFactory { get; }

		protected abstract ITesselation<TPosition, TKey> Tesselation { get; }

		protected abstract IVertexGenerator<TVertex, TNode, TPosition, TKey> VertexGenerator { get; }

		public void Tesselate()
		{
			var state = new State();

			// First Pass: Discover Areas/Shapes/Nodes
			var availablePoints = new List<TPosition>(Tesselation.Points());
			var inactivePoints = new List<TPosition>(availablePoints.Count);
			while (availablePoints.Count > 0)
			{
				var reference = availablePoints[0];
				var key = Tesselation.Key(reference);
				var shape = state.CreateShape(key);

				var activePoints = new List<TPosition>() { availablePoints[0] };
				var innerInActivePoints = new List<TPosition>();
				while (activePoints.Count > 0)
				{
					var newActivePoints = new List<TPosition>();
					for (int apIndex = 0; apIndex < activePoints.Count; apIndex++)
					{
						var v = activePoints[apIndex];
						availablePoints.Remove(v);
						shape.AddNode(NodeFactory, v);

						int index = 0;
						foreach (var vs in Tesselation.Surround(v))
						{
							try
							{
								if (!Tesselation.Valid(vs) || !Tesselation.Tesselate(index))
								{
									continue;
								}
							}
							finally
							{
								index++;
							}

							if (!Equals(key, Tesselation.Key(vs)))
								continue;

							if (!(inactivePoints.Contains(vs) || innerInActivePoints.Contains(vs) || newActivePoints.Contains(vs)))
								newActivePoints.Add(vs);
						}
					}

					innerInActivePoints.AddRange(activePoints);
					activePoints = newActivePoints;
				}

				inactivePoints.AddRange(innerInActivePoints);
			}

			// Second Pass: Connect Shapes
			foreach (var shape in state.Shapes)
			{
				foreach (var node in shape.Nodes)
				{
					int set = 0;
					int index = 0;
					foreach (var neighbor in Tesselation.Surround(node.Point))
					{
						var lookupShape = default(Shape<TNode, TPosition, TKey>);
						if (Tesselation.Valid(neighbor))
							lookupShape = state.GetShape(neighbor);
						else
							lookupShape = null;
						set *= 2;
						if (lookupShape != shape)
							set++;
						node[index++] = lookupShape != null ? lookupShape.Area : null;
					}

					if (EdgeGenerator.OuterNode(set))
					{
						shape.AddOuterNode(node);
					}
				}
			}

			// Third Pass: Create Mesh/Edges per Area
			foreach (var area in state.Areas)
			{
				var meshTransform = MeshTransformProvider.Get(area);
				var colliderTransform = ColliderTransformProvider.Get(area);
				foreach (var shape in state.ShapesByArea(area))
				{
					shape.CreateMesh(VertexGenerator, meshTransform);
					shape.CreateCollider(EdgeGenerator, colliderTransform);
				}
				meshTransform.Finish();
				colliderTransform.Finish();
			}
		}

		private class State
		{
			private Dictionary<TKey, Area<TKey>> areaCache = new Dictionary<TKey, Area<TKey>>();
			private List<Area<TKey>> areas = new List<Area<TKey>>();
			private Dictionary<TPosition, TNode> positionNodeLookup = new Dictionary<TPosition, TNode>();
			private Dictionary<TPosition, Shape<TNode, TPosition, TKey>> positionShapeLookup = new Dictionary<TPosition, Shape<TNode, TPosition, TKey>>();
			private List<Shape<TNode, TPosition, TKey>> shapes = new List<Shape<TNode, TPosition, TKey>>();

			public List<Area<TKey>> Areas { get { return areas; } }

			public List<Shape<TNode, TPosition, TKey>> Shapes { get { return shapes; } }

			public TNode AddNodeToShape(Shape<TNode, TPosition, TKey> shape, INodeFactory<TNode, TPosition, TKey> nodeFactory, TPosition position)
			{
				var node = nodeFactory.Create(shape.Area, position);
				shape.AddNode(node);
				positionShapeLookup[position] = shape;
				return node;
			}

			public Shape<TNode, TPosition, TKey> CreateShape(TKey key)
			{
				return CreateShape(GetOrCreateArea(key));
			}

			public Shape<TNode, TPosition, TKey> CreateShape(Area<TKey> area)
			{
				var shape = new Shape<TNode, TPosition, TKey>(area);
				shapes.Add(shape);
				return shape;
			}

			public Area<TKey> GetOrCreateArea(TKey key)
			{
				var area = default(Area<TKey>);
				if (areaCache.TryGetValue(key, out area)) return area;
				foreach (var item in Areas)
				{
					if (Equals(item.Key, key))
					{
						return areaCache[key] = item;
					}
				}
				area = new Area<TKey>(key);
				areas.Add(area);
				return area;
			}

			public Shape<TNode, TPosition, TKey> GetShape(TPosition position)
			{
				var shape = default(Shape<TNode, TPosition, TKey>);
				if (positionShapeLookup.TryGetValue(position, out shape)) return shape;
				var count = shapes.Count;
				for (int i = 0; i < count; i++)
				{
					shape = shapes[i];
					var nodeCount = shape.Nodes.Count;
					for (int j = 0; j < nodeCount; j++)
					{
						var node = shape.Nodes[j];
						if (Equals(node.Point, position))
						{
							return positionShapeLookup[position] = shape;
						}
					}
				}
				return null;
			}

			public IEnumerable<Shape<TNode, TPosition, TKey>> ShapesByArea(Area<TKey> area)
			{
				var shapeCount = shapes.Count;
				for (int i = 0; i < shapeCount; i++)
				{
					if (Equals(shapes[i].Area, area))
					{
						yield return shapes[i];
					}
				}
			}

			public IEnumerable<Shape<TNode, TPosition, TKey>> ShapesByKey(TKey key)
			{
				var shapeCount = shapes.Count;
				for (int i = 0; i < shapeCount; i++)
				{
					if (Equals(shapes[i].Area.Key, key))
					{
						yield return shapes[i];
					}
				}
			}
		}
	}
}
