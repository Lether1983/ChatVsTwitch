using System.Collections.Generic;

namespace UnityTesselation.Contracts.Generators
{
	public interface IGenerator<T, TNode, TPosition, TKey> where TNode : INode<TPosition, TKey>
	{
		IEnumerable<T> Generate(TNode node);
	}
}
