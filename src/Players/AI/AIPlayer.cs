using System;
using System.Threading.Tasks;

namespace Chess
{
	public class AIPlayer : Player
	{
		private MoveChooser MoveChooser;

		public AIConstants Constants;

		public AIPlayer(BoardNode board, Piece.Colour colour) : base(board, colour)
		{
			MoveChooser = new MoveChooser(colour);
			Constants = AIConstants.Default;
		}

		public override Task<Move> GetMoveAsync()
		{
			return Task.FromResult(MoveChooser.ChooseMove(Board.Board, Board.LegalMoves));
		}

		protected override Task<Piece.PieceType> _Promote(Position position)
		{
			// TODO Improve

			return Task.FromResult(Piece.PieceType.Queen);
		}
	}
}