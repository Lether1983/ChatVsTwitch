using System.Collections.Generic;
using UnityTesselation.Contracts.Generators;

namespace UnityTesselation.Defaults
{
	public sealed class DisabledColliderTransform<TPosition> : DisabledTransform<Edge<TPosition>>, IColliderTransform<TPosition> { }

	public sealed class DisabledMeshTransform<TVertex> : DisabledTransform<TVertex>, IMeshTransform<TVertex> { }

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
