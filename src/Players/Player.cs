using System;
using System.Linq;

namespace Chess
{
	public abstract class Player
	{
		public readonly BoardNode Board;
		public readonly Piece.Colour Colour;

		protected Player(BoardNode board, Piece.Colour colour)
		{
			Board = board;
			Colour = colour;
		}

		/// <summary>
		/// Called when a piece is selected to maybe be moved.
		/// This is used for visualising available moves.
		/// </summary>
		public event Action<Position?> MoveFromSelected;

		protected void InvokeMoveFromSelected(Position? position)
		{
			MoveFromSelected?.Invoke(position);
		}

		public abstract void GetMove(Action<Move> onMoveChosen);

		public void Promote(Position position, Action<Piece.PieceType> onPromoted)
		{
			_Promote(position, promotion =>
			{
				if (!Chess.Board.PromotionOutcomes.Contains(promotion))
				{
					throw new InvalidOperationException("Can only promote to rooks, knights, bishops, or queens");
				}

				onPromoted(promotion);
			});
		}

		protected abstract void _Promote(Position position, Action<Piece.PieceType> onPromoted);
	}
}