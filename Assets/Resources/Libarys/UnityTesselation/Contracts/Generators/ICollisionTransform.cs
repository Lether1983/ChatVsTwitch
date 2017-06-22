namespace UnityTesselation.Contracts.Generators
{
	public interface IColliderTransform<TCollision> : ITransform<TCollision> where TCollision : ICollision
	{
	}
}
