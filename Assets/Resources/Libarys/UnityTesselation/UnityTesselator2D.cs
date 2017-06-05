using UnityEngine;
using UnityTesselation.Contracts;

namespace UnityTesselation
{
	public abstract class UnityTesselator2D<TVertex, TNode, TKey> : UnityTesselator<TVertex, TNode, Vector2, TKey> where TNode : INode<Vector2, TKey>
	{
	}
}
