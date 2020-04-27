using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Moves
{
	public static class Queen
	{
		public static IEnumerable<Position> GetMoves(Board board, Position position)
		{
			return Rook.GetMoves(board, position).Concat(Bishop.GetMoves(board, position));
		}
	}
}