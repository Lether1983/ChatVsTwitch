using UnityTesselation.Contracts.Generators;

namespace UnityTesselation.Contracts.Factories
{
	public interface IColliderTransformProvider<TCollision, TKey> where TCollision : ICollision
	{
		IColliderTransform<TCollision> Get(Area<TKey> area);
	}
}
