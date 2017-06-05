namespace UnityTesselation.Contracts.Generators
{
	public interface IVertexGenerator<TVertex, TNode, TPosition, TKey> : IGenerator<TVertex, TNode, TPosition, TKey> where TNode : INode<TPosition, TKey>
	{
	}
}
