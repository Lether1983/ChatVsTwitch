using UnityTesselation.Contracts.Generators;

namespace UnityTesselation.Contracts.Factories
{
	public interface IColliderTransformProvider<TPosition, TKey>
	{
		IColliderTransform<TPosition> Get(Area<TKey> area);
	}
}
