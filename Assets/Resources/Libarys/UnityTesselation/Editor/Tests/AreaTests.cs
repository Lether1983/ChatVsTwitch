using NUnit.Framework;
using UnityEngine;

namespace UnityTesselation
{
	[TestFixture]
	public class AreaTests
	{
		[Test]
		public void ZeroArea2D()
		{
			Assert.True(PolygonExtensions.CoordinatesFormLine(Vector2.zero, Vector2.left, Vector2.right));
		}

		[Test]
		public void ZeroArea2DFast()
		{
			Assert.True(PolygonExtensions.CoordinatesFormLine(true, Vector2.zero, Vector2.left, Vector2.right));
		}

		[Test]
		public void ZeroArea3D()
		{
			Assert.True(PolygonExtensions.CoordinatesFormLine(Vector3.zero, Vector3.left, Vector3.right));
		}
	}
}
