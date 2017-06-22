namespace UnityTesselation.Contracts.Generators
{
	public interface ICollisionGenerator<TCollision, TNode, TPosition, TKey> : IGenerator<TCollision, TNode, TPosition, TKey>
		where TCollision : ICollision
		where TNode : INode<TPosition, TKey>
	{
		bool OuterNode(int set);
	}
}
