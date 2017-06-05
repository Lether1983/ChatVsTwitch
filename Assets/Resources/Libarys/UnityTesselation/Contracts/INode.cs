namespace UnityTesselation.Contracts
{
	public interface INode<TPosition, TKey>
	{
		Area<TKey> this[int index] { get; set; }

		int Neighbours { get; }

		TPosition Point { get; }

		Area<TKey> Self { get; }
	}
}
