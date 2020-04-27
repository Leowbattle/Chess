using System;
using System.Collections.Generic;

namespace Chess.Moves
{
	public static class Pawn
	{
		public static IEnumerable<Position> GetMoves(Board board, Position position)
		{
			var piece = board[position].Value;

			var direction = piece.Owner == Piece.Colour.White ? 1 : -1;

			// Move pawn forwards
			var pos = new Position(position.File, position.Rank + direction);
			if (pos.Valid() && !board[pos].HasValue)
			{
				yield return pos;
			}

			// Move pawn 2 spaces forwards if it is it's first move
			pos = new Position(position.File, position.Rank + direction * 2);
			if (!piece.HasEverMoved && pos.Valid() && !board[pos].HasValue && !board[new Position(position.File, position.Rank + direction)].HasValue)
			{
				yield return pos;
			}

			// Take diagonally
			pos = new Position(position.File + 1, position.Rank + direction);
			if (pos.Valid() && board[pos]?.Owner == piece.Owner.Opposite())
			{
				yield return pos;
			}

			pos = new Position(position.File - 1, position.Rank + direction);
			if (pos.Valid() && board[pos]?.Owner == piece.Owner.Opposite())
			{
				yield return pos;
			}

			// TODO Promotion
		}
	}
}