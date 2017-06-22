using UnityTesselation.Contracts;

namespace UnityTesselation
{
	public struct Face<TPosition> : ICollision
	{
		private TPosition v1, v2, v3, v4;

		public TPosition V1 { get { return v1; } }

		public TPosition V2 { get { return v2; } }

		public TPosition V3 { get { return v3; } }

		public TPosition V4 { get { return v4; } }

		public Face(TPosition v1, TPosition v2, TPosition v3, TPosition v4)
		{
			this.v1 = v1;
			this.v2 = v2;
			this.v3 = v3;
			this.v4 = v4;
		}
	}
}
