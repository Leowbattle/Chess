using Godot;
using System;

namespace Chess
{
	public class Menu : Control
	{
		private PlayerTypeSelector White;
		private PlayerTypeSelector Black;

		public override void _Ready()
		{
			White = GetNode<PlayerTypeSelector>("Menu/White");
			Black = GetNode<PlayerTypeSelector>("Menu/Black");

			var play = GetNode<Button>("Menu/Play");
			play.Connect("pressed", this, nameof(Play));
		}

		private Player CreatePlayer(PlayerTypeSelector.PlayerType type, BoardNode board, Piece.Colour colour)
		{
			switch (type)
			{
				case PlayerTypeSelector.PlayerType.Human:
					return new HumanPlayer(board, colour);
				case PlayerTypeSelector.PlayerType.Computer:
					return new AIPlayer(board, colour);
				case PlayerTypeSelector.PlayerType.Random:
					return new RandomPlayer(board, colour);
				default:
					throw new ApplicationException($"Invalid player type {type}");
			}
		}

		private void Play()
		{
			var scn = GD.Load<PackedScene>("src/Board/Board.tscn");
			var game = (BoardNode)scn.Instance();

			game.WhitePlayer = CreatePlayer(White.Selection, game, Piece.Colour.White);
			game.BlackPlayer = CreatePlayer(Black.Selection, game, Piece.Colour.Black);

			GetParent().AddChild(game);
			QueueFree();
		}
	}
}
