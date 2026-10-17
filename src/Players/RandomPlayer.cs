using System;
using System.Linq;

namespace Chess
{
	public class RandomPlayer : Player
	{
		private Random Random;

		public RandomPlayer(BoardNode board, Piece.Colour colour) : base(board, colour)
		{
			Random = new Random();
		}

		public override void GetMove(Action<Move> onMoveChosen)
		{
			var move = Board.LegalMoves.ElementAt(Random.Next(0, Board.LegalMoves.Count - 1));
			InvokeMoveFromSelected(move.From);

			onMoveChosen(move);
		}

		protected override void _Promote(Position position, Action<Piece.PieceType> onPromoted)
		{
			var promotion = Chess.Board.PromotionOutcomes[Random.Next(0, Chess.Board.PromotionOutcomes.Length - 1)];
			onPromoted(promotion);
		}
	}
}