using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
	public class Board
	{
		public const int Size = 8;

		private Piece?[] board;

		public Board()
		{
			board = new Piece?[Size * Size]
			{
				new Piece(Piece.PieceType.Rook, Piece.Colour.White),
				new Piece(Piece.PieceType.Knight, Piece.Colour.White),
				new Piece(Piece.PieceType.Bishop, Piece.Colour.White),
				new Piece(Piece.PieceType.Queen, Piece.Colour.White),
				new Piece(Piece.PieceType.King, Piece.Colour.White),
				new Piece(Piece.PieceType.Bishop, Piece.Colour.White),
				new Piece(Piece.PieceType.Knight, Piece.Colour.White),
				new Piece(Piece.PieceType.Rook, Piece.Colour.White),

				new Piece(Piece.PieceType.Pawn, Piece.Colour.White),
				new Piece(Piece.PieceType.Pawn, Piece.Colour.White),
				new Piece(Piece.PieceType.Pawn, Piece.Colour.White),
				new Piece(Piece.PieceType.Pawn, Piece.Colour.White),
				new Piece(Piece.PieceType.Pawn, Piece.Colour.White),
				new Piece(Piece.PieceType.Pawn, Piece.Colour.White),
				new Piece(Piece.PieceType.Pawn, Piece.Colour.White),
				new Piece(Piece.PieceType.Pawn, Piece.Colour.White),

				null, null, null, null, null, null, null, null,
				null, null, null, null, null, null, null, null,
				null, null, null, null, null, null, null, null,
				null, null, null, null, null, null, null, null,

				new Piece(Piece.PieceType.Pawn, Piece.Colour.Black),
				new Piece(Piece.PieceType.Pawn, Piece.Colour.Black),
				new Piece(Piece.PieceType.Pawn, Piece.Colour.Black),
				new Piece(Piece.PieceType.Pawn, Piece.Colour.Black),
				new Piece(Piece.PieceType.Pawn, Piece.Colour.Black),
				new Piece(Piece.PieceType.Pawn, Piece.Colour.Black),
				new Piece(Piece.PieceType.Pawn, Piece.Colour.Black),
				new Piece(Piece.PieceType.Pawn, Piece.Colour.Black),

				new Piece(Piece.PieceType.Rook, Piece.Colour.Black),
				new Piece(Piece.PieceType.Knight, Piece.Colour.Black),
				new Piece(Piece.PieceType.Bishop, Piece.Colour.Black),
				new Piece(Piece.PieceType.Queen, Piece.Colour.Black),
				new Piece(Piece.PieceType.King, Piece.Colour.Black),
				new Piece(Piece.PieceType.Bishop, Piece.Colour.Black),
				new Piece(Piece.PieceType.Knight, Piece.Colour.Black),
				new Piece(Piece.PieceType.Rook, Piece.Colour.Black)
			};
		}

		private Board(Piece?[] board)
		{
			this.board = board;
		}

		public ref Piece? this[Position pos] => ref board[pos.Rank * Size + pos.File];

		public Board Clone()
		{
			return new Board((Piece?[])board.Clone());
		}
		public IEnumerable<Position> GetPositions(Piece.Colour player)
		{
			for (int i = 0; i < Size; i++)
			{
				for (int j = 0; j < Size; j++)
				{
					var piece = this[new Position(j, i)];
					if (piece?.Owner == player)
					{
						yield return new Position(j, i);
					}
				}
			}
		}

		private IEnumerable<Position> _GetMoves(Position position)
		{
			switch (this[position]?.Type)
			{
				case Piece.PieceType.Pawn:
					return Moves.Pawn.GetMoves(this, position);
				case Piece.PieceType.Rook:
					return Moves.Rook.GetMoves(this, position);
				case Piece.PieceType.Knight:
					return Moves.Knight.GetMoves(this, position);
				case Piece.PieceType.Bishop:
					return Moves.Bishop.GetMoves(this, position);
				case Piece.PieceType.Queen:
					return Moves.Queen.GetMoves(this, position);
				case Piece.PieceType.King:
					return Moves.King.GetMoves(this, position);

				case null:
					return Enumerable.Empty<Position>();

				default:
					throw new ApplicationException();
			}
		}

		public Position KingPosition(Piece.Colour player)
		{
			for (int i = 0; i < Size; i++)
			{
				for (int j = 0; j < Size; j++)
				{
					var position = new Position(j, i);
					if (this[position] is Piece piece)
					{
						if (piece.Type == Piece.PieceType.King && piece.Owner == player)
						{
							return position;
						}
					}
				}
			}

			throw new ApplicationException($"{player} does not have a king");
		}

		/// <summary>
		/// Returns true if a piece at "position" can be taken by a piece owned by "from"
		/// </summary>
		/// <param name="position"></param>
		/// <param name="from"></param>
		/// <returns></returns>
		public bool UnderAttack(Position position, Piece.Colour from)
		{
			foreach (var move in _GetMoves(from))
			{
				if (move.To == position)
				{
					return true;
				}
			}

			return false;
		}

		public event Action Checkmate;
		public event Action Stalemate;

		public static readonly Piece.PieceType[] PromotionOutcomes = new Piece.PieceType[]
		{
			Piece.PieceType.Rook,
			Piece.PieceType.Knight,
			Piece.PieceType.Bishop,
			Piece.PieceType.Queen
		};

		public IEnumerable<Move> GetMoves(Position position)
		{
			var moves = _GetMoves(position).Select(to => new Move(position, to));

			// Check for check
			moves = moves.Where(move =>
			{
				foreach (var outcome in PromotionOutcomes)
				{
					var b = Clone();
					b.DoMove(move, p => outcome);
					foreach (var p in b.GetPositions(this[position].Value.Owner.Opposite()))
					{
						foreach (var m in b._GetMoves(p))
						{
							if (b[m]?.Type == Piece.PieceType.King)
							{
								return false;
							}
						}
					}
				}

				return true;
			});

			return moves;
		}

		public IEnumerable<Move> GetMoves(Piece.Colour player)
		{
			var moves = GetPositions(player)
				.Select(position => GetMoves(position))
				.SelectMany(x => x);

			if (!moves.Any())
			{
				var kingpos = KingPosition(player);
				if (UnderAttack(kingpos, player.Opposite()))
				{
					Checkmate?.Invoke();
				}
				else
				{
					Stalemate?.Invoke();
				}
			}

			return moves;
		}

		public IEnumerable<Move> _GetMoves(Piece.Colour player)
		{
			return GetPositions(player)
				.Select(position => _GetMoves(position).Select(p => new Move(position, p)))
				.SelectMany(x => x);
		}

		/// <summary>
		/// Do a move, return the taken piece
		/// </summary>
		/// <param name="move"></param>
		/// <returns></returns>
		public Piece? DoMove(Move move, Func<Position, Piece.PieceType> promote)
		{
			var taken = this[move.To];

			var mover = this[move.From].Value;
			mover.HasEverMoved = true;

			var promotionRank = mover.Owner == Piece.Colour.White ? 7 : 0;
			if (mover.Type == Piece.PieceType.Pawn && move.To.Rank == promotionRank)
			{
				mover.Type = promote(move.To);
			}

			this[move.To] = mover;

			this[move.From] = null;

			return taken;
		}
	}
}