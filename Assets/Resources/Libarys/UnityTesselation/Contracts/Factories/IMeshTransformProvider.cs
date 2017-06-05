using UnityTesselation.Contracts.Generators;

namespace UnityTesselation.Contracts.Factories
{
	public interface IMeshTransformProvider<TVertex, TKey>
	{
		IMeshTransform<TVertex> Get(Area<TKey> area);
	}
}
