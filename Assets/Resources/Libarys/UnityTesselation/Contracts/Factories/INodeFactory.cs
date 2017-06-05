namespace UnityTesselation.Contracts.Factories
{
	public interface INodeFactory<TNode, TPosition, TKey> where TNode : INode<TPosition, TKey>
	{
		TNode Create(Area<TKey> self, TPosition point);
	}
}
