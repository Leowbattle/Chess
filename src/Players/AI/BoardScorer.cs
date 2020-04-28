using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
	/// <summary>
	/// Assigns a game state a score that says how *good* it is for the AI.
	/// </summary>
	public class BoardScorer
	{
		private readonly Board Board;
		private readonly Piece.Colour Player;
		private AIConstants Constants;

		public BoardScorer(Board board, Piece.Colour player, AIConstants constants)
		{
			Board = board;
			Player = player;
			Constants = constants;
		}

		public static float Score(Board board, Piece.Colour player, AIConstants constants)
		{
			return new BoardScorer(board, player, constants).Score();
		}

		private float Score()
		{
			float score = 0;

			score += ScoreMaterial(Player);
			score += ScoreMaterial(Player.Opposite()) * Constants.OpponentMaterialMultiplier;

			score += ScoreMiddleSquares();

			if (!Board.GetPositions(Player.Opposite()).Any(p => Board[p].Value.Type == Piece.PieceType.King))
			{
				score += Constants.CheckmateScore;
			}

			return score;
		}

		/// <summary>
		/// The more pieces "player" has, the more points they have.
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		private float ScoreMaterial(Piece.Colour player)
		{
			var material = Board.GetPositions(player);

			float score = 0;

			foreach (var position in material)
			{
				switch (Board[position].Value.Type)
				{
					case Piece.PieceType.Pawn:
						score += Constants.PawnScore;
						break;
					case Piece.PieceType.Knight:
						score += Constants.KnightScore;
						break;
					case Piece.PieceType.Bishop:
						score += Constants.BishopScore;
						break;
					case Piece.PieceType.Rook:
						score += Constants.RookScore;
						break;
					case Piece.PieceType.Queen:
						score += Constants.QueenScore;
						break;
					default:
						break;
				}
			}

			return score;
		}

		private static Position[] MiddleSquares = new Position[]
		{
			('e', 4), ('e', 5),
			('d', 4), ('d', 5)
		};

		/// <summary>
		/// Gain points if "Player" has pieces in the middle squares.
		/// Lose points if the other player has pieces in the middle squares.
		/// </summary>
		/// <returns></returns>
		private float ScoreMiddleSquares()
		{
			float score = 0;

			foreach (var square in MiddleSquares)
			{
				if (Board[square] is Piece piece)
				{
					if (piece.Owner == Player)
					{
						score += Constants.MiddleSquareScore;
					}
					else
					{
						score += Constants.MiddleSquareScore * Constants.OpponentMiddleSquareMultiplier;
					}
				}
			}

			return score;
		}
	}

	// static class PositionExt
	// {
	// 	public static float DistanceToCentre(this Position position)
	// 	{
	// 		var dx = 3.5f - position.File;
	// 		var dy = 3.5f - position.Rank;

	// 		return (float)Math.Sqrt(dx * dx + dy * dy);
	// 	}
	// }
}