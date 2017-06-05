using UnityEngine;
using UnityTesselation.Contracts;

namespace UnityTesselation
{
	public abstract class UnityTesselator3D<TVertex, TNode, TKey> : UnityTesselator<TVertex, TNode, Vector3, TKey> where TNode : INode<Vector3, TKey>
	{
	}
}
