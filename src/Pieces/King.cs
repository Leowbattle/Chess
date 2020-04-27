using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Moves
{
	public static class King
	{
		public static IEnumerable<Position> GetMoves(Board board, Position position)
		{
			var piece = board[position].Value;

			var positions = new Position[]
			{
				new Position(position.File - 1, position.Rank + 1),
				new Position(position.File, position.Rank + 1),
				new Position(position.File + 1, position.Rank + 1),

				new Position(position.File - 1, position.Rank),
				new Position(position.File + 1, position.Rank),

				new Position(position.File - 1, position.Rank - 1),
				new Position(position.File, position.Rank - 1),
				new Position(position.File + 1, position.Rank - 1)
			};

			foreach (var pos in positions)
			{
				if (pos.Valid() && (board[pos] == null || board[pos]?.Owner == piece.Owner.Opposite()))
				{
					yield return pos;
				}
			}
		}
	}
}