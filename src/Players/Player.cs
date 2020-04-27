using System;
using System.Threading.Tasks;
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

		public abstract Task<Move> GetMoveAsync();

		public async Task<Piece.PieceType> Promote(Position position)
		{
			var promotion = await _Promote(position);
			if (Chess.Board.PromotionOutcomes.Contains(promotion))
			{
				return promotion;
			}

			throw new InvalidOperationException("Can only promote to rooks, knights, bishops, or queens");
		}

		protected abstract Task<Piece.PieceType> _Promote(Position position);
	}
}