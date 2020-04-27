using System;

namespace Chess
{
	public struct Piece
	{
		public enum PieceType
		{
			Pawn,
			Rook,
			Knight,
			Bishop,
			Queen,
			King
		}

		public enum Colour
		{
			White,
			Black
		}

		public PieceType Type;
		public Colour Owner;
		public bool HasEverMoved;

		public Piece(PieceType type, Colour owner) : this()
		{
			Type = type;
			Owner = owner;
			HasEverMoved = false;
		}

		public override string ToString()
		{
			return $"{Owner} {Type}";
		}
	}

	public static class ColourExt
	{
		public static Piece.Colour Opposite(this Piece.Colour colour)
		{
			return colour == Piece.Colour.White ? Piece.Colour.Black : Piece.Colour.White;
		}
	}
}