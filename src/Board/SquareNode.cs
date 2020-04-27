using Godot;
using System;

namespace Chess
{
	public class SquareNode : Node2D
	{
		private BoardNode Board;

		public readonly Piece.Colour Colour;
		public new readonly Position Position;

		private ColorRect Rect;

		public const int SquareSize = 90;

		private static readonly Color WhiteColour = Colors.Beige;
		private static readonly Color BlackColour = Colors.DarkGray;

		public event Action<Position> Clicked;

		public SquareNode()
		{ }

		public SquareNode(BoardNode board, Piece.Colour colour, Position position)
		{
			Board = board;

			Colour = colour;
			Position = position;
		}

		public override void _Ready()
		{
			Rect = new ColorRect();
			AddChild(Rect);
			Rect.Color = Colour == Piece.Colour.White ? WhiteColour : BlackColour;
			Rect.RectSize = new Vector2(SquareSize, SquareSize);

			var pos = BoardNode.ScreenCoords(Position);
			Rect.RectPosition = pos;
		}

		public override void _Input(InputEvent e)
		{
			if (e is InputEventMouseButton m)
			{
				if (Input.IsMouseButtonPressed((int)ButtonList.Left) && Rect.GetRect().HasPoint(m.Position))
				{
					Clicked?.Invoke(Position);
				}
			}
		}
	}
}
