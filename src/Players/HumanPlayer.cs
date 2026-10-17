using System;
using System.Linq;

namespace Chess
{
	public class HumanPlayer : Player
	{
		private Action<Move> OnMoveChosen;
		private Position? From;

		public HumanPlayer(BoardNode board, Piece.Colour colour) : base(board, colour)
		{
			board.SquareClicked += OnSquareClicked;
		}

		public override void GetMove(Action<Move> onMoveChosen)
		{
			OnMoveChosen = onMoveChosen;
			From = null;
			InvokeMoveFromSelected(null);
		}

		private void OnSquareClicked(Position position)
		{
			if (Board.CurrentPlayer != Colour || OnMoveChosen == null)
			{
				return;
			}

			if (From == null)
			{
				if (Board.LegalMoves.Any(m => m.From == position))
				{
					From = position;
					InvokeMoveFromSelected(position);
				}
				return;
			}

			if (Board.Board[position]?.Owner == Colour)
			{
				From = position;
				InvokeMoveFromSelected(position);
				return;
			}

			var move = new Move(From.Value, position);
			From = null;

			if (!Board.LegalMoves.Contains(move))
			{
				InvokeMoveFromSelected(null);
				return;
			}

			var callback = OnMoveChosen;
			OnMoveChosen = null;
			InvokeMoveFromSelected(null);
			callback(move);
		}

		protected override void _Promote(Position position, Action<Piece.PieceType> onPromoted)
		{
			Board.UI.PromotionUI.GetChoice(onPromoted);
		}
	}
}