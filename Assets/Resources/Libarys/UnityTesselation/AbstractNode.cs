using UnityTesselation.Contracts;

namespace UnityTesselation
{
	public abstract class AbstractNode<TPosition, TKey> : INode<TPosition, TKey>
	{
		private TPosition point;
		private Area<TKey> self;

		public Area<TKey> this[int index]
		{
			get { return GetShape(index); }
			set { SetShape(index, value); }
		}

		public abstract int Neighbours { get; }

		public TPosition Point { get { return point; } }

		public Area<TKey> Self { get { return self; } }

		public AbstractNode(Area<TKey> self, TPosition point)
		{
			this.self = self;
			this.point = point;
		}

		protected abstract Area<TKey> GetShape(int index);

		protected abstract void SetShape(int index, Area<TKey> value);
	}
}
