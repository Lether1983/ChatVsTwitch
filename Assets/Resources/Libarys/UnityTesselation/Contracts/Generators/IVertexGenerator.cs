namespace UnityTesselation.Contracts.Generators
{
	public interface IVertexGenerator<TVertex, TNode, TPosition, TKey> : IGenerator<TVertex, TNode, TPosition, TKey>
		where TVertex : IVertex
		where TNode : INode<TPosition, TKey>
	{
	}
}
