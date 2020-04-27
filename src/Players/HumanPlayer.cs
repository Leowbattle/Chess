using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Godot;

namespace Chess
{
	public class HumanPlayer : Player
	{
		private TaskCompletionSource<Position> SquareClickedTcs;

		private async Task<Position> SquareClicked()
		{
			var p = await SquareClickedTcs.Task;

			// If this is not here "SquareClickedTcs = new TaskCompletionSource<Position>();"
			// does not get a chance to run after "SquareClickedTcs.SetResult(p);"
			await Task.Yield();

			return p;
		}

		public HumanPlayer(BoardNode board, Piece.Colour colour) : base(board, colour)
		{
			SquareClickedTcs = new TaskCompletionSource<Position>();
			board.SquareClicked += p =>
			{
				if (Board.CurrentPlayer != Colour)
				{
					return;
				}

				SquareClickedTcs.SetResult(p);
				SquareClickedTcs = new TaskCompletionSource<Position>();
			};
		}

		public override async Task<Move> GetMoveAsync()
		{
			while (true)
			{
				Position from;
				while (true)
				{
					from = await SquareClicked();
					if (Board.LegalMoves.Any(m => m.From == from))
					{
						break;
					}
				}
				InvokeMoveFromSelected(from);

				Position to;
				while (true)
				{
					to = await SquareClicked();

					if (Board.Board[to]?.Owner == Colour)
					{
						from = to;
						InvokeMoveFromSelected(from);
					}
					else
					{
						break;
					}
				}

				var move = new Move(from, to);

				if (!Board.LegalMoves.Contains(move))
				{
					InvokeMoveFromSelected(null);
					continue;
				}

				return move;
			}
		}

		protected override Task<Piece.PieceType> _Promote(Position position)
		{
			throw new NotImplementedException();
		}
	}
}