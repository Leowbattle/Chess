using System;
using System.Collections.Generic;
using Godot;

namespace Chess
{
	public static class Resources
	{
		public static Dictionary<Piece.PieceType, Texture2D> WhitePieces = new Dictionary<Piece.PieceType, Texture2D>()
		{
			[Piece.PieceType.Pawn] = GD.Load<Texture2D>("src/Pieces/white_pawn.svg"),
			[Piece.PieceType.Rook] = GD.Load<Texture2D>("src/Pieces/white_rook.svg"),
			[Piece.PieceType.Knight] = GD.Load<Texture2D>("src/Pieces/white_knight.svg"),
			[Piece.PieceType.Bishop] = GD.Load<Texture2D>("src/Pieces/white_bishop.svg"),
			[Piece.PieceType.Queen] = GD.Load<Texture2D>("src/Pieces/white_queen.svg"),
			[Piece.PieceType.King] = GD.Load<Texture2D>("src/Pieces/white_king.svg")
		};

		public static Dictionary<Piece.PieceType, Texture2D> BlackPieces = new Dictionary<Piece.PieceType, Texture2D>()
		{
			[Piece.PieceType.Pawn] = GD.Load<Texture2D>("src/Pieces/black_pawn.svg"),
			[Piece.PieceType.Rook] = GD.Load<Texture2D>("src/Pieces/black_rook.svg"),
			[Piece.PieceType.Knight] = GD.Load<Texture2D>("src/Pieces/black_knight.svg"),
			[Piece.PieceType.Bishop] = GD.Load<Texture2D>("src/Pieces/black_bishop.svg"),
			[Piece.PieceType.Queen] = GD.Load<Texture2D>("src/Pieces/black_queen.svg"),
			[Piece.PieceType.King] = GD.Load<Texture2D>("src/Pieces/black_king.svg")
		};
	}
}