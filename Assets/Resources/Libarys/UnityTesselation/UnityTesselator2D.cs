using System.Collections.Generic;
using UnityEngine;
using UnityTesselation.Contracts;

namespace UnityTesselation
{
	public abstract class UnityTesselator2D<TVertex, TNode, TKey> : UnityTesselator<Edge<Vector2>, TVertex, TNode, Vector2, TKey>
		where TVertex : IVertex
		where TNode : INode<Vector2, TKey>
	{
		private EqualityComparer<TKey> keyComparer = EqualityComparer<TKey>.Default;
		private Vector2EqualityComparer positionComparer;

		protected override IEqualityComparer<TKey> KeyComparer { get { return keyComparer; } }

		protected override IEqualityComparer<Vector2> PositionComparer { get { return positionComparer; } }

		private struct Vector2EqualityComparer : IEqualityComparer<Vector2>
		{
			public bool Equals(Vector2 x, Vector2 y)
			{
				return x.x == y.x && x.y == y.y;
			}

			public int GetHashCode(Vector2 obj)
			{
				unchecked
				{
					var xHash = obj.x.GetHashCode();
					var yHash = obj.y.GetHashCode();

					var hashCode = 137 + (xHash >> 16 | xHash << 16);
					hashCode = (hashCode * 397) + (yHash >> 16 | yHash << 16);
					return hashCode;
				}
			}
		}
	}
}
