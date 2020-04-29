using System;

namespace Chess
{
	public struct Move
	{
		public Position From;
		public Position To;

		public Move(Position from, Position to)
		{
			From = from;
			To = to;
		}

		public static bool operator ==(Move a, Move b)
		{
			return a.From == b.From && a.To == b.To;
		}

		public static bool operator !=(Move a, Move b)
		{
			return !(a == b);
		}

		public override string ToString()
		{
			return $"Move from {From} to {To}";
		}
	}
}