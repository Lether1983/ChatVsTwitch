using UnityTesselation.Contracts.Generators;

namespace UnityTesselation.Contracts.Factories
{
	public interface IMeshTransformProvider<TVertex, TKey> where TVertex : IVertex
	{
		IMeshTransform<TVertex> Get(Area<TKey> area);
	}
}
