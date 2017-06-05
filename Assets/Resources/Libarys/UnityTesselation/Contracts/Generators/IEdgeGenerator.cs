namespace UnityTesselation.Contracts.Generators
{
	public interface IEdgeGenerator<TNode, TPosition, TKey> : IGenerator<Edge<TPosition>, TNode, TPosition, TKey> where TNode : INode<TPosition, TKey>
	{
		bool OuterNode(int set);
	}
}
