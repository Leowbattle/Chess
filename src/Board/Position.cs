using System;

namespace Chess
{
	public struct Position
	{
		/// <summary>
		/// X coordinate from left to right
		/// </summary>
		public int File;

		/// <summary>
		/// Y coordinate from bottom to top
		/// </summary>
		public int Rank;

		public Position(int file, int rank)
		{
			// if (!Valid(file, rank))
			// {
			// 	throw new ArgumentOutOfRangeException($"{file}, {rank}");
			// }

			File = file;
			Rank = rank;
		}

		public Position(char file, int rank) : this((int)(char.ToLower(file) - 'a'), rank - 1)
		{

		}

		public bool Valid()
		{
			return File >= 0 && File < 8 && Rank >= 0 && Rank < 8;
		}

		public static implicit operator Position((char file, int rank) pos)
		{
			return new Position(pos.file, pos.rank);
		}

		public override string ToString()
		{
			const string Files = "abcdefgh";
			return $"{Files[File]}{Rank + 1}";
		}

		public static bool operator ==(Position p, Position q)
		{
			return p.Rank == q.Rank && p.File == q.File;
		}

		public static bool operator !=(Position p, Position q)
		{
			return !(p == q);
		}
	}
}