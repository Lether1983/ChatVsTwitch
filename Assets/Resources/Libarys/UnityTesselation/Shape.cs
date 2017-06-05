using System.Collections.Generic;
using UnityTesselation.Contracts;

namespace UnityTesselation
{
	public class Shape<TNode, TPosition, TKey> where TNode : INode<TPosition, TKey>
	{
		private Area<TKey> area;
		private List<TNode> nodes = new List<TNode>();
		private List<TNode> outernodes = new List<TNode>();

		public Area<TKey> Area { get { return area; } }

		public IList<TNode> Nodes { get { return nodes.AsReadOnly(); } }

		public IList<TNode> OuterNodes { get { return outernodes.AsReadOnly(); } }

		public Shape(Area<TKey> area)
		{
			this.area = area;
		}

		public void AddNode(TNode node)
		{
			nodes.Add(node);
		}

		public void AddOuterNode(TNode node)
		{
			outernodes.Add(node);
		}
	}
}
