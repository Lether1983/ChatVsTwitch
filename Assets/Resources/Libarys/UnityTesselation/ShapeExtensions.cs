using UnityTesselation.Contracts;
using UnityTesselation.Contracts.Factories;
using UnityTesselation.Contracts.Generators;

namespace UnityTesselation
{
	public static class ShapeExtensions
	{
		public static void AddNode<TNode, TPosition, TKey>(
			this Shape<TNode, TPosition, TKey> shape,
			INodeFactory<TNode, TPosition, TKey> factory,
			TPosition point) where TNode : INode<TPosition, TKey>
		{
			shape.AddNode(factory.Create(shape.Area, point));
		}

		public static void CreateCollider<TCollision, TNode, TPosition, TKey>(
			this Shape<TNode, TPosition, TKey> shape,
			ICollisionGenerator<TCollision, TNode, TPosition, TKey> collisionGenerator,
			IColliderTransform<TCollision> colliderTransform)
			where TCollision : ICollision
			where TNode : INode<TPosition, TKey>
		{
			foreach (var node in shape.OuterNodes)
			{
				colliderTransform.Consume(collisionGenerator.Generate(node));
			}
		}

		public static void CreateMesh<TVertex, TNode, TPosition, TKey>(
			this Shape<TNode, TPosition, TKey> shape,
			IVertexGenerator<TVertex, TNode, TPosition, TKey> vertexGenerator,
			IMeshTransform<TVertex> meshTransform)
			where TVertex : IVertex
			where TNode : INode<TPosition, TKey>
		{
			foreach (var node in shape.Nodes)
			{
				meshTransform.Consume(vertexGenerator.Generate(node));
			}
		}
	}
}
