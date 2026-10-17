using Godot;
using System;

namespace Chess
{
	public class PromotionUI : VBoxContainer
	{
		private BoardNode Board;

		private Action<Piece.PieceType> OnChoice;

		public void GetChoice(Action<Piece.PieceType> onChoice)
		{
			if (Board.CurrentPlayer == Piece.Colour.White)
			{
				Rook.TextureNormal = Resources.WhitePieces[Piece.PieceType.Rook];
				Knight.TextureNormal = Resources.WhitePieces[Piece.PieceType.Knight];
				Bishop.TextureNormal = Resources.WhitePieces[Piece.PieceType.Bishop];
				Queen.TextureNormal = Resources.WhitePieces[Piece.PieceType.Queen];
			}
			else
			{
				Rook.TextureNormal = Resources.BlackPieces[Piece.PieceType.Rook];
				Knight.TextureNormal = Resources.BlackPieces[Piece.PieceType.Knight];
				Bishop.TextureNormal = Resources.BlackPieces[Piece.PieceType.Bishop];
				Queen.TextureNormal = Resources.BlackPieces[Piece.PieceType.Queen];
			}

			OnChoice = onChoice;
			Visible = true;
		}

		private TextureButton Rook;
		private TextureButton Knight;
		private TextureButton Bishop;
		private TextureButton Queen;

		public override void _Ready()
		{
			Board = GetParent<UI>().GetParent<BoardNode>();

			Rook = GetNode<TextureButton>("Choices/Rook");
			Knight = GetNode<TextureButton>("Choices/Knight");
			Bishop = GetNode<TextureButton>("Choices/Bishop");
			Queen = GetNode<TextureButton>("Choices/Queen");

			Rook.Connect("pressed", this, nameof(RookChosen));
			Knight.Connect("pressed", this, nameof(KnightChosen));
			Bishop.Connect("pressed", this, nameof(BishopChosen));
			Queen.Connect("pressed", this, nameof(QueenChosen));
		}

		private void RookChosen() => Choose(Piece.PieceType.Rook);

		private void KnightChosen() => Choose(Piece.PieceType.Knight);

		private void BishopChosen() => Choose(Piece.PieceType.Bishop);

		private void QueenChosen() => Choose(Piece.PieceType.Queen);

		private void Choose(Piece.PieceType type)
		{
			Visible = false;

			var callback = OnChoice;
			OnChoice = null;
			callback?.Invoke(type);
		}
	}
}
