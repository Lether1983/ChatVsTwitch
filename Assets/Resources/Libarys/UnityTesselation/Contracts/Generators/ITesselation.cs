using System.Collections.Generic;

namespace UnityTesselation.Contracts.Generators
{
	public interface ITesselation<TPosition, TKey>
	{
		TKey Key(TPosition point);

		IEnumerable<TPosition> Points();

		IEnumerable<TPosition> Surround(TPosition v);

		bool Tesselate(int index);

		bool Valid(TPosition point);
	}
}
