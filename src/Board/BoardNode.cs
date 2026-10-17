using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
	public class BoardNode : Control
	{
		public const int BoardSize = SquareNode.SquareSize * Board.Size;

		public Board Board { get; private set; }

		public SquareNode[,] Squares = new SquareNode[8, 8];

		public UI UI { get; private set; }

		public Player WhitePlayer;
		public Player BlackPlayer;

		//private Piece.Colour currentPlayer;
		public Piece.Colour CurrentPlayer;
		public Player Current => CurrentPlayer == Piece.Colour.White ? WhitePlayer : BlackPlayer;

		public HashSet<Move> LegalMoves { get; private set; }

		public event Action<Position> SquareClicked;

		public event Action<Piece.Colour> Checkmate;
		public event Action Stalemate;

		private PieceDrawer Drawer;
		private bool gameRunning;

		public override void _Ready()
		{
			gameRunning = true;

			Board = new Board();
			Board.Checkmate += winner =>
			{
				gameRunning = false;

				GD.Print($"Checkmate! {winner} wins.");
				Checkmate?.Invoke(winner);
			};
			Board.Stalemate += () =>
			{
				gameRunning = false;

				GD.Print("Stalemate!");
				Stalemate?.Invoke();
			};

			UI = GetNode<UI>("UI");

			var squares = GetNode<Control>("Squares");
			var colour = Piece.Colour.Black;
			for (int i = 0; i < Board.Size; i++)
			{
				for (int j = 0; j < Board.Size; j++)
				{
					var square = new SquareNode(this, colour, new Position(j, i));
					square.Clicked += position =>
					{
						SquareClicked?.Invoke(position);
					};
					Squares[j, i] = square;
					squares.AddChild(square);

					colour = colour.Opposite();
				}
				colour = colour.Opposite();
			}

			// WhitePlayer = new HumanPlayer(this, Piece.Colour.White);
			//BlackPlayer = new HumanPlayer(this, Piece.Colour.Black);

			//WhitePlayer = new RandomPlayer(this, Piece.Colour.White);
			//BlackPlayer = new RandomPlayer(this, Piece.Colour.Black);

			//WhitePlayer = new AIPlayer(this, Piece.Colour.White);
			// BlackPlayer = new AIPlayer(this, Piece.Colour.Black);

			Drawer = new PieceDrawer(this);
			squares.AddChild(Drawer);

			CurrentPlayer = Piece.Colour.White;
			StartTurn();
		}

		private void StartTurn()
		{
			if (!gameRunning)
			{
				return;
			}

			LegalMoves = Board.GetMoves(CurrentPlayer).ToHashSet();
			Current.GetMove(OnMoveChosen);
		}

		private void OnMoveChosen(Move move)
		{
			if (!gameRunning)
			{
				return;
			}

			GD.Print($"{CurrentPlayer} wants to move {move}");

			if (IsPromotion(move))
			{
				Current.Promote(move.To, promotion => ApplyMove(move, promotion));
			}
			else
			{
				ApplyMove(move, Piece.PieceType.Queen);
			}
		}

		private bool IsPromotion(Move move)
		{
			var piece = Board[move.From];
			if (piece?.Type != Piece.PieceType.Pawn)
			{
				return false;
			}

			var promotionRank = piece.Value.Owner == Piece.Colour.White ? 7 : 0;
			return move.To.Rank == promotionRank;
		}

		private void ApplyMove(Move move, Piece.PieceType promotion)
		{
			Board.DoMove(move, _ => promotion);
			Drawer.Update();

			CurrentPlayer = CurrentPlayer.Opposite();

			if (!gameRunning)
			{
				return;
			}

			// Give the engine a frame to draw the move before the next turn.
			GetTree().CreateTimer(0.05f)
				.Connect("timeout", this, nameof(StartTurn));
		}

		public static Vector2 ScreenCoords(Position pos)
		{
			return new Vector2(
				pos.File * SquareNode.SquareSize,
				BoardSize - (pos.Rank + 1) * SquareNode.SquareSize
			);
		}
	}
}
