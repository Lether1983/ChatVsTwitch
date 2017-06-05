using System.Collections.Generic;

namespace UnityTesselation.Contracts.Generators
{
	public interface ITransform<T>
	{
		void Consume(IEnumerable<T> items);

		void Finish();
	}
}
