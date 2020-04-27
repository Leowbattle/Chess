using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Moves
{
	public static class Rook
	{
		public static IEnumerable<Position> GetMoves(Board board, Position position)
		{
			var piece = board[position].Value;

			for (int i = position.File + 1; i < Board.Size; i++)
			{
				var pos = new Position(i, position.Rank);
				if (board[pos] == null || board[pos]?.Owner == piece.Owner.Opposite())
				{
					yield return pos;
				}
				if (board[pos].HasValue)
				{
					break;
				}
			}

			for (int i = position.File - 1; i >= 0; i--)
			{
				var pos = new Position(i, position.Rank);
				if (board[pos] == null || board[pos]?.Owner == piece.Owner.Opposite())
				{
					yield return pos;
				}
				if (board[pos].HasValue)
				{
					break;
				}
			}

			for (int i = position.Rank + 1; i < Board.Size; i++)
			{
				var pos = new Position(position.File, i);
				if (board[pos] == null || board[pos]?.Owner == piece.Owner.Opposite())
				{
					yield return pos;
				}
				if (board[pos].HasValue)
				{
					break;
				}
			}

			for (int i = position.Rank - 1; i >= 0; i--)
			{
				var pos = new Position(position.File, i);
				if (board[pos] == null || board[pos]?.Owner == piece.Owner.Opposite())
				{
					yield return pos;
				}
				if (board[pos].HasValue)
				{
					break;
				}
			}
		}
	}
}