using UnityEngine;

namespace UnityTesselation
{
	public static class PolygonExtensions
	{
		public static bool CoordinatesFormLine(params Vector3[] vertices)
		{
			var length = vertices.Length;
			Vector3 result = Vector3.zero;
			for (int p = length - 1, q = 0; q < length; p = q++)
			{
				result += Vector3.Cross(vertices[q], vertices[p]);
			}
			result *= 0.5f;

			return Mathf.Approximately(result.magnitude, 0);
		}

		public static bool CoordinatesFormLine(bool fast = false, params Vector2[] vertices)
		{
			var area = 0f;
			if (vertices.Length == 3 && fast)
			{
				var a = vertices[0];
				var b = vertices[1];
				var c = vertices[2];
				//If the area of a triangle created from three points is zero, they must be in a line.
				area = a.x * (b.y - c.y) +
					b.x * (c.y - a.y) +
					c.x * (a.y - b.y);
			}
			else
			{
				var length = vertices.Length;
				Vector3 result = Vector3.zero;
				for (int p = length - 1, q = 0; q < length; p = q++)
				{
					result += Vector3.Cross(vertices[q], vertices[p]);
				}
				area = result.z * 0.5f;
			}
			return Mathf.Approximately(area, 0);
		}
	}
}
