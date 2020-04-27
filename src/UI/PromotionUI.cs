using Godot;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
	public class PromotionUI : VBoxContainer
	{
		private BoardNode Board;

		private TaskCompletionSource<Piece.PieceType> Tcs;

		public async Task<Piece.PieceType> GetChoice()
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

			Visible = true;

			var choice = await Tcs.Task;

			Visible = false;

			return choice;
		}

		private TextureButton Rook;
		private TextureButton Knight;
		private TextureButton Bishop;
		private TextureButton Queen;

		public override void _Ready()
		{
			Board = GetParent<UI>().GetParent<BoardNode>();

			Tcs = new TaskCompletionSource<Piece.PieceType>();

			Rook = GetNode<TextureButton>("Choices/Rook");
			Knight = GetNode<TextureButton>("Choices/Knight");
			Bishop = GetNode<TextureButton>("Choices/Bishop");
			Queen = GetNode<TextureButton>("Choices/Queen");

			Rook.Connect("pressed", this, nameof(RookChosen));
			Knight.Connect("pressed", this, nameof(KnightChosen));
			Bishop.Connect("pressed", this, nameof(BishopChosen));
			Queen.Connect("pressed", this, nameof(QueenChosen));
		}

		private void RookChosen()
		{
			Tcs.SetResult(Piece.PieceType.Rook);
			Tcs = new TaskCompletionSource<Piece.PieceType>();
		}

		private void KnightChosen()
		{
			Tcs.SetResult(Piece.PieceType.Knight);
			Tcs = new TaskCompletionSource<Piece.PieceType>();
		}

		private void BishopChosen()
		{
			Tcs.SetResult(Piece.PieceType.Bishop);
			Tcs = new TaskCompletionSource<Piece.PieceType>();
		}

		private void QueenChosen()
		{
			Tcs.SetResult(Piece.PieceType.Queen);
			Tcs = new TaskCompletionSource<Piece.PieceType>();
		}
	}
}
