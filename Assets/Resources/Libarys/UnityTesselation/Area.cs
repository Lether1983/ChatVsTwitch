namespace UnityTesselation
{
	public sealed class Area<T>
	{
		private T key;

		public T Key { get { return key; } }

		public Area(T key)
		{
			this.key = key;
		}
	}
}
