using System.Collections.Generic;
using System.Linq;
using UnityTesselation.Contracts;
using UnityTesselation.Contracts.Generators;

namespace UnityTesselation.Defaults
{
	public class DisabledEdgeGenerator<TNode, TPosition, TKey> : DisabledGenerator<Edge<TPosition>, TNode, TPosition, TKey>, IEdgeGenerator<TNode, TPosition, TKey> where TNode : INode<TPosition, TKey>
	{
		public bool OuterNode(int set)
		{
			return false;
		}
	}

	public class DisabledGenerator<TEnumerable, TNode, TPosition, TKey> where TNode : INode<TPosition, TKey>
	{
		public IEnumerable<TEnumerable> Generate(TNode node)
		{
			return Enumerable.Empty<TEnumerable>();
		}
	}

	public class DisabledVertexGenerator<TVertex, TNode, TPosition, TKey> : DisabledGenerator<TVertex, TNode, TPosition, TKey>, IVertexGenerator<TVertex, TNode, TPosition, TKey> where TNode : INode<TPosition, TKey> { }
}
