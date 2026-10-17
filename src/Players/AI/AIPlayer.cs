using System;

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

		public override void GetMove(Action<Move> onMoveChosen)
		{
			var move = MoveChooser.ChooseMove(Board.Board, Board.LegalMoves);
			onMoveChosen(move);
		}

		protected override void _Promote(Position position, Action<Piece.PieceType> onPromoted)
		{
			// TODO Improve

			onPromoted(Piece.PieceType.Queen);
		}
	}
}
