using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Moves
{
	public static class Bishop
	{
		public static IEnumerable<Position> GetMoves(Board board, Position position)
		{
			var piece = board[position].Value;

			for (int i = 1; ; i++)
			{
				var pos = new Position(position.File + i, position.Rank + i);
				if (!pos.Valid())
				{
					break;
				}

				if (board[pos] == null || board[pos]?.Owner == piece.Owner.Opposite())
				{
					yield return pos;
				}
				if (board[pos].HasValue)
				{
					break;
				}
			}

			for (int i = 1; ; i++)
			{
				var pos = new Position(position.File - i, position.Rank - i);
				if (!pos.Valid())
				{
					break;
				}

				if (board[pos] == null || board[pos]?.Owner == piece.Owner.Opposite())
				{
					yield return pos;
				}
				if (board[pos].HasValue)
				{
					break;
				}
			}

			for (int i = 1; ; i++)
			{
				var pos = new Position(position.File - i, position.Rank + i);
				if (!pos.Valid())
				{
					break;
				}

				if (board[pos] == null || board[pos]?.Owner == piece.Owner.Opposite())
				{
					yield return pos;
				}
				if (board[pos].HasValue)
				{
					break;
				}
			}

			for (int i = 1; ; i++)
			{
				var pos = new Position(position.File + i, position.Rank - i);
				if (!pos.Valid())
				{
					break;
				}

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