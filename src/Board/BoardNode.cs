using Godot;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
	public class BoardNode : Control
	{
		public const int BoardSize = SquareNode.SquareSize * Board.Size;

		public Board Board { get; private set; }

		public SquareNode[,] Squares = new SquareNode[8, 8];

		public Player WhitePlayer;
		public Player BlackPlayer;

		//private Piece.Colour currentPlayer;
		public Piece.Colour CurrentPlayer;
		public Player Current => CurrentPlayer == Piece.Colour.White ? WhitePlayer : BlackPlayer;

		public HashSet<Move> LegalMoves { get; private set; }

		public event Action<Position> SquareClicked;

		public async override void _Ready()
		{
			Board = new Board();
			Board.Checkmate += () =>
			{
				GD.Print("Checkmate!");
			};
			Board.Stalemate += () =>
			{
				GD.Print("Stalemate!");
			};

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
					AddChild(square);

					colour = colour.Opposite();
				}
				colour = colour.Opposite();
			}

			WhitePlayer = new HumanPlayer(this, Piece.Colour.White);
			BlackPlayer = new HumanPlayer(this, Piece.Colour.Black);

			//WhitePlayer = new RandomPlayer(this, Piece.Colour.White);
			//BlackPlayer = new RandomPlayer(this, Piece.Colour.Black);

			var drawer = new PieceDrawer(this);
			AddChild(drawer);

			CurrentPlayer = Piece.Colour.White;
			while (true)
			{
				LegalMoves = Board.GetMoves(CurrentPlayer).ToHashSet();

				var move = await Current.GetMoveAsync();
				await Task.Delay(25);

				GD.Print($"{CurrentPlayer} wants to move {move}");
				Board.DoMove(move, p => Current.Promote(p).Result);
				drawer.Update();

				CurrentPlayer = CurrentPlayer.Opposite();
			}
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
