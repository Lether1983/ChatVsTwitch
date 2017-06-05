using System.Collections.Generic;
using System.Linq;

namespace UnityTesselation
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int size)
		{
			var i = 0;
			return
				from element in source
				group element by i++ / size into splitGroups
				select splitGroups.AsEnumerable();
		}
	}
}
