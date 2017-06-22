using System.Collections.Generic;
using UnityEngine;
using UnityTesselation.Contracts;

namespace UnityTesselation
{
	public abstract class UnityTesselator3D<TVertex, TNode, TKey> : UnityTesselator<Face<Vector3>, TVertex, TNode, Vector3, TKey>
		where TVertex : IVertex
		where TNode : INode<Vector3, TKey>
	{
		private EqualityComparer<TKey> keyComparer = EqualityComparer<TKey>.Default;
		private Vector3EqualityComparer positionComparer;

		protected override IEqualityComparer<TKey> KeyComparer { get { return keyComparer; } }

		protected override IEqualityComparer<Vector3> PositionComparer { get { return positionComparer; } }

		private struct Vector3EqualityComparer : IEqualityComparer<Vector3>
		{
			public bool Equals(Vector3 x, Vector3 y)
			{
				return x.x == y.x && x.y == y.y && x.z == y.z;
			}

			public int GetHashCode(Vector3 obj)
			{
				unchecked
				{
					return ((23 * 31 + (int)obj.x) * 31 + (int)obj.y) * 31 + (int)obj.z;

					//var xHash = obj.x.GetHashCode();
					//var yHash = obj.y.GetHashCode();
					//var zHash = obj.z.GetHashCode();

					//return (((xHash >> 16) * 397) + (yHash >> 16)) * 397 + (zHash >> 16);
				}
			}
		}
	}
}
