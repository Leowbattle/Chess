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

		public override string ToString()
		{
			return $"Move from {From} to {To}";
		}
	}
}