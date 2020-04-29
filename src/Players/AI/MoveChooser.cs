using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
	public class MoveChooser
	{
		private Piece.Colour Player;

		public MoveChooser(Piece.Colour player)
		{
			Player = player;
		}

		/// <summary>
		/// Chooses the *best* move from a set of moves.
		/// </summary>
		/// <param name="board"></param>
		/// <param name="moves"></param>
		/// <returns></returns>
		public Move ChooseMove(Board board, HashSet<Move> moves)
		{
			AIConstants constants = AIConstants.Default;

			float highestScore = float.NegativeInfinity;
			Move bestMove = moves.First();

			foreach (var move in moves)
			{
				var b = board.Clone();
				b.DoMove(move, _ => Piece.PieceType.Queen);

				float min = float.PositiveInfinity;

				var nextMoves = b.GetMoves(Player.Opposite());
				foreach (var nm in nextMoves)
				{
					var b2 = b.Clone();
					b2.DoMove(nm, _ => Piece.PieceType.Queen);
					var score = BoardScorer.Score(b2, Player, constants);
					if (score < min)
					{
						min = score;
					}
				}

				if (min > highestScore)
				{
					highestScore = min;
					bestMove = move;
				}

				// var score = BoardScorer.Score(b, Player, constants);

				// if (score > highestScore)
				// {
				// 	highestScore = score;
				// 	bestMove = move;
				// }
			}

			return bestMove;
		}
	}
}