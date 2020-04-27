using System;
using System.Collections.Generic;
using Godot;

namespace Chess
{
	public static class Resources
	{
		public static Dictionary<Piece.PieceType, Texture> WhitePieces = new Dictionary<Piece.PieceType, Texture>()
		{
			[Piece.PieceType.Pawn] = GD.Load<Texture>("src/Pieces/white_pawn.svg"),
			[Piece.PieceType.Rook] = GD.Load<Texture>("src/Pieces/white_rook.svg"),
			[Piece.PieceType.Knight] = GD.Load<Texture>("src/Pieces/white_knight.svg"),
			[Piece.PieceType.Bishop] = GD.Load<Texture>("src/Pieces/white_bishop.svg"),
			[Piece.PieceType.Queen] = GD.Load<Texture>("src/Pieces/white_queen.svg"),
			[Piece.PieceType.King] = GD.Load<Texture>("src/Pieces/white_king.svg")
		};

		public static Dictionary<Piece.PieceType, Texture> BlackPieces = new Dictionary<Piece.PieceType, Texture>()
		{
			[Piece.PieceType.Pawn] = GD.Load<Texture>("src/Pieces/black_pawn.svg"),
			[Piece.PieceType.Rook] = GD.Load<Texture>("src/Pieces/black_rook.svg"),
			[Piece.PieceType.Knight] = GD.Load<Texture>("src/Pieces/black_knight.svg"),
			[Piece.PieceType.Bishop] = GD.Load<Texture>("src/Pieces/black_bishop.svg"),
			[Piece.PieceType.Queen] = GD.Load<Texture>("src/Pieces/black_queen.svg"),
			[Piece.PieceType.King] = GD.Load<Texture>("src/Pieces/black_king.svg")
		};
	}
}