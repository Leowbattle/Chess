using System;
using System.Linq;
using Godot;

namespace Chess
{
	public class PieceDrawer : Node2D
	{
		private BoardNode Board;

		public PieceDrawer(BoardNode board)
		{
			Board = board;

			Board.WhitePlayer.MoveFromSelected += MoveFromSelected;
			Board.BlackPlayer.MoveFromSelected += MoveFromSelected;
		}

		private Position? MoveFrom;
		private void MoveFromSelected(Position? pos)
		{
			MoveFrom = pos;
			Update();
		}

		public override void _Draw()
		{
			VisualiseMoves();
			DrawPieces();
		}

		private void DrawPieces()
		{
			for (int i = 0; i < Chess.Board.Size; i++)
			{
				for (int j = 0; j < Chess.Board.Size; j++)
				{
					var position = new Position(j, i);

					if (Board.Board[position] is Piece piece)
					{
						DrawTexture(PieceTexture(piece), BoardNode.ScreenCoords(position));
					}
				}
			}
		}

		private void VisualiseMoves()
		{
			if (MoveFrom.HasValue)
			{
				DrawRect(new Rect2(BoardNode.ScreenCoords(MoveFrom.Value), new Vector2(SquareNode.SquareSize, SquareNode.SquareSize)), Colors.Blue);
			}

			if (Board.LegalMoves != null)
			{
				foreach (var move in Board.LegalMoves.Where(m => m.From == MoveFrom))
				{
					var colour = Colors.Limegreen;
					if (Board.Board[move.To].HasValue)
					{
						colour = Colors.Red;
					}
					DrawRect(new Rect2(BoardNode.ScreenCoords(move.To), new Vector2(SquareNode.SquareSize, SquareNode.SquareSize)), colour);
				}
			}
		}

		private Texture PieceTexture(Piece piece)
		{
			return piece.Owner == Piece.Colour.White
				? Resources.WhitePieces[piece.Type]
				: Resources.BlackPieces[piece.Type];
		}
	}
}