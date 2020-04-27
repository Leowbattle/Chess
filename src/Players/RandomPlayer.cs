using System;
using System.Threading.Tasks;
using System.Linq;
using Godot;

namespace Chess
{
	public class RandomPlayer : Player
	{
		private Random Random;

		public RandomPlayer(BoardNode board, Piece.Colour colour) : base(board, colour)
		{
			Random = new Random();
		}

		public override Task<Move> GetMoveAsync()
		{
			var move = Board.LegalMoves.ElementAt(Random.Next(0, Board.LegalMoves.Count - 1));
			InvokeMoveFromSelected(move.From);

			return Task.FromResult(move);
		}

		protected override Task<Piece.PieceType> _Promote(Position position)
		{
			var promotion = Chess.Board.PromotionOutcomes[Random.Next(0, Chess.Board.PromotionOutcomes.Length - 1)];
			return Task.FromResult(promotion);
		}
	}
}