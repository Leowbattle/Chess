namespace Chess
{
	public class AIConstants
	{
		public static AIConstants Default => new AIConstants();

		// https://en.wikipedia.org/wiki/Chess_piece_relative_value

		public float PawnScore = 1;
		public float KnightScore = 3;
		public float BishopScore = 3;
		public float RookScore = 5;
		public float QueenScore = 9;

		/// <summary>
		/// Multiplier for many points you lose for the other player having pieces.
		/// </summary>
		public float OpponentMaterialMultiplier = -0.8f;

		/// <summary>
		/// How many points you gain for having pieces in the middle squares.
		/// </summary>
		public float MiddleSquareScore = 1;

		/// <summary>
		/// Multiplier for many points you lose when the other player has pieces in the middle squares.
		/// </summary>
		public float OpponentMiddleSquareMultiplier = -0.8f;

		public float CheckmateScore = 1000;
	}
}