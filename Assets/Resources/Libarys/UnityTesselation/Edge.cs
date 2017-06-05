namespace UnityTesselation
{
	public struct Edge<TPosition>
	{
		private TPosition v1, v2;

		public TPosition V1 { get { return v1; } }

		public TPosition V2 { get { return v2; } }

		public Edge(TPosition v1, TPosition v2)
		{
			this.v1 = v1;
			this.v2 = v2;
		}

		public override bool Equals(object obj)
		{
			if (obj is Edge<TPosition>)
			{
				var edge = (Edge<TPosition>)obj;
				//An edge is equal regardless of which order it's points are in
				return (Equals(edge.v1, v1) && Equals(edge.v2, v2)) || (Equals(edge.v2, v1) && Equals(edge.v1, v2));
			}

			return false;
		}

		public override int GetHashCode()
		{
			return v1.GetHashCode() ^ v2.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("[" + v1 + "->" + v2 + "]");
		}
	}
}
