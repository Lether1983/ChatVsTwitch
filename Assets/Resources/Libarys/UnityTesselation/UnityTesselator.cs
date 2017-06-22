using System.Collections.Generic;
using UnityEngine;
using UnityTesselation.Contracts;
using UnityTesselation.Contracts.Factories;
using UnityTesselation.Contracts.Generators;

namespace UnityTesselation
{
	public abstract class UnityTesselator<TCollision, TVertex, TNode, TPosition, TKey> : MonoBehaviour
		where TCollision : ICollision
		where TVertex : IVertex
		where TNode : INode<TPosition, TKey>
	{
		protected abstract IColliderTransformProvider<TCollision, TKey> ColliderTransformProvider { get; }

		protected abstract ICollisionGenerator<TCollision, TNode, TPosition, TKey> CollisionGenerator { get; }

		protected abstract IEqualityComparer<TKey> KeyComparer { get; }

		protected abstract IMeshTransformProvider<TVertex, TKey> MeshTransformProvider { get; }

		protected abstract INodeFactory<TNode, TPosition, TKey> NodeFactory { get; }

		protected abstract IEqualityComparer<TPosition> PositionComparer { get; }

		protected abstract ITesselation<TPosition, TKey> Tesselation { get; }

		protected abstract IVertexGenerator<TVertex, TNode, TPosition, TKey> VertexGenerator { get; }

		public void Tesselate()
		{
			var state = new State(this);
			// First Pass: Discover Areas/Shapes/Nodes
			FirstPass(state);

			// Second Pass: Connect Shapes
			SecondPass(state);

			// Third Pass: Create Mesh/Edges per Area
			ThirdPass(state);
		}

		private static TPosition GetReference(HashSet<TPosition> skippedPoints)
		{
			var reference = default(TPosition);
			using (var skippedPointsEnumerator = ((IEnumerable<TPosition>)skippedPoints).GetEnumerator())
			{
				skippedPointsEnumerator.MoveNext();
				reference = skippedPointsEnumerator.Current;
			}
			skippedPoints.Remove(reference);
			return reference;
		}

		private void FirstPass(State state)
		{
			var skippedPoints = new HashSet<TPosition>(PositionComparer) { default(TPosition) };

			var activePoints = new HashSet<TPosition>(PositionComparer);
			var newActivePoints = new HashSet<TPosition>(PositionComparer);
			var inactivePoints = new HashSet<TPosition>(PositionComparer);

			var index = 0;
			var testIndex = 0;
			var reference = default(TPosition);
			var key = default(TKey);
			var shape = default(Shape<TNode, TPosition, TKey>);

			while (skippedPoints.Count > 0)
			{
				reference = GetReference(skippedPoints);
				key = Tesselation.Key(reference);
				shape = state.CreateShape(key);

				activePoints.Clear();
				activePoints.Add(reference);

				while (activePoints.Count > 0)
				{
					newActivePoints.Clear();

					foreach (var v in activePoints)
					{
						skippedPoints.Remove(v);
						inactivePoints.Add(v);
						state.AddNodeToShape(shape, NodeFactory, v);

						index = 0;
						foreach (var vs in Tesselation.Surround(v))
						{
							testIndex = index++;
							if (Tesselation.Valid(vs) && Tesselation.Tesselate(testIndex) && !(newActivePoints.Contains(vs) || inactivePoints.Contains(vs)))
							{
								if (KeyComparer.Equals(key, Tesselation.Key(vs)))
								{
									newActivePoints.Add(vs);
								}
								else
								{
									skippedPoints.Add(vs);
								}
							}
						}
					}
					
					activePoints.Clear();
					foreach (var item in newActivePoints)
					{
						activePoints.Add(item);
					}
				}
			}
		}

		private void SecondPass(State state)
		{
			int set = 0;
			int index = 0;
			var lookupShape = default(Shape<TNode, TPosition, TKey>);

			var shape = default(Shape<TNode, TPosition, TKey>);
			var node = default(TNode);
			var tesselationSurroundEnumerable = default(IEnumerable<TPosition>);
			var tesselationSurroundEnumerator = default(IEnumerator<TPosition>);
			var neighbor = default(TPosition);

			for (int shapeIndex = state.Shapes.Count - 1; shapeIndex >= 0; shapeIndex--)
			{
				shape = state.Shapes[shapeIndex];
				for (int nodeIndex = shape.Nodes.Count - 1; nodeIndex >= 0; nodeIndex--)
				{
					set = 0;
					index = 0;
					node = shape.Nodes[nodeIndex];

					tesselationSurroundEnumerable = Tesselation.Surround(node.Point);
					tesselationSurroundEnumerator = tesselationSurroundEnumerable.GetEnumerator();
					while (tesselationSurroundEnumerator.MoveNext())
					{
						neighbor = tesselationSurroundEnumerator.Current;
						if (Tesselation.Valid(neighbor))
						{
							lookupShape = state.GetShape(neighbor);
						}
						else
						{
							lookupShape = null;
						}
						set *= 2;
						if (lookupShape != shape)
						{
							set++;
						}
						node[index++] = lookupShape != null ? lookupShape.Area : null;
					}
					tesselationSurroundEnumerator.Dispose();

					if (CollisionGenerator.OuterNode(set))
					{
						shape.AddOuterNode(node);
					}
				}
			}
		}

		private void ThirdPass(State state)
		{
			var meshTransform = default(IMeshTransform<TVertex>);
			var colliderTransform = default(IColliderTransform<TCollision>);
			foreach (var area in state.Areas)
			{
				meshTransform = MeshTransformProvider.Get(area);
				colliderTransform = ColliderTransformProvider.Get(area);
				foreach (var shape in state.GetShapes(area))
				{
					shape.CreateMesh(VertexGenerator, meshTransform);
					shape.CreateCollider(CollisionGenerator, colliderTransform);
				}
				meshTransform.Finish();
				colliderTransform.Finish();
			}
		}

		private class State
		{
			private Dictionary<TKey, Area<TKey>> areaCache;
			private List<Area<TKey>> areas = new List<Area<TKey>>();
			private UnityTesselator<TCollision, TVertex, TNode, TPosition, TKey> parent;
			private Dictionary<TPosition, Shape<TNode, TPosition, TKey>> positionShapeLookup;
			private Dictionary<TKey, List<Shape<TNode, TPosition, TKey>>> shapeCache;
			private List<Shape<TNode, TPosition, TKey>> shapes = new List<Shape<TNode, TPosition, TKey>>();

			public List<Area<TKey>> Areas { get { return areas; } }

			public List<Shape<TNode, TPosition, TKey>> Shapes { get { return shapes; } }

			public State(UnityTesselator<TCollision, TVertex, TNode, TPosition, TKey> parent)
			{
				this.parent = parent;

				areaCache = new Dictionary<TKey, Area<TKey>>(4, parent.KeyComparer);
				shapeCache = new Dictionary<TKey, List<Shape<TNode, TPosition, TKey>>>(4, parent.KeyComparer);
				positionShapeLookup = new Dictionary<TPosition, Shape<TNode, TPosition, TKey>>(32768, parent.PositionComparer);
			}

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
				shapeCache[area.Key].Add(shape);
				return shape;
			}

			public Area<TKey> GetOrCreateArea(TKey key)
			{
				var area = default(Area<TKey>);
				if (areaCache.TryGetValue(key, out area)) return area;

				area = new Area<TKey>(key);
				areas.Add(area);
				areaCache[key] = area;
				shapeCache[key] = new List<Shape<TNode, TPosition, TKey>>(1024);
				return area;
			}

			public Shape<TNode, TPosition, TKey> GetShape(TPosition position)
			{
				var shape = default(Shape<TNode, TPosition, TKey>);
				if (positionShapeLookup.TryGetValue(position, out shape))
					return shape;
				Debug.Log("Shape not found. Returning null");
				return null;
			}

			public List<Shape<TNode, TPosition, TKey>> GetShapes(TKey key)
			{
				var shapeList = default(List<Shape<TNode, TPosition, TKey>>);
				if (shapeCache.TryGetValue(key, out shapeList))
					return shapeList;
				Debug.Log("Key not found. Returning empty list");
				return new List<Shape<TNode, TPosition, TKey>>();
			}

			public List<Shape<TNode, TPosition, TKey>> GetShapes(Area<TKey> area)
			{
				return GetShapes(area.Key);
			}
		}
	}
}
