using Godot;
using System;

namespace Chess
{
	public class UI : Control
	{
		private BoardNode Board;

		public PromotionUI PromotionUI { get; private set; }

		public override void _Ready()
		{
			Board = GetParent<BoardNode>();

			Board.Checkmate += OnCheckmate;
			Board.Stalemate += OnStalemate;

			PromotionUI = GetNode<PromotionUI>("Promotion");

			GetNode<Button>("Checkmate/Menu").Connect("pressed", this, nameof(Menu));
			GetNode<Button>("Stalemate/Menu").Connect("pressed", this, nameof(Menu));
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

		private void Menu()
		{
			var scn = GD.Load<PackedScene>("src/Menu.tscn");
			var menu = (Menu)scn.Instance();
			GetParent().AddChild(menu);
			QueueFree();
		}
	}
}
