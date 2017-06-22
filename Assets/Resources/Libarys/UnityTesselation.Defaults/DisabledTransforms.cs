using System.Collections.Generic;
using UnityTesselation.Contracts;
using UnityTesselation.Contracts.Generators;

namespace UnityTesselation.Defaults
{
	public sealed class DisabledColliderTransform<TCollision> : DisabledTransform<TCollision>, IColliderTransform<TCollision> where TCollision : ICollision { }

	public sealed class DisabledMeshTransform<TVertex> : DisabledTransform<TVertex>, IMeshTransform<TVertex> where TVertex : IVertex { }

	public class DisabledTransform<T>
	{
		public void Consume(IEnumerable<T> items)
		{
		}

		public void Finish()
		{
		}
	}
}
