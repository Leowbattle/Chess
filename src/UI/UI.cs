using Godot;
using System;

namespace Chess
{
	public class UI : Control
	{
		private BoardNode Board;

		public override void _Ready()
		{
			Board = GetParent<BoardNode>();

			Board.Checkmate += OnCheckmate;
			Board.Stalemate += OnStalemate;
		}

		private void OnCheckmate(Piece.Colour winner)
		{
			GetNode<Control>("Checkmate").Visible = true;

			var whoLabel = GetNode<Label>("Checkmate/Who");
			whoLabel.Text = $"{winner} wins!";
		}

		private void OnStalemate()
		{
			GetNode<Control>("Stalemate").Visible = true;
		}
	}
}
