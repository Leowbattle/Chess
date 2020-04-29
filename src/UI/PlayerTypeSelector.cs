using Godot;
using System;

namespace Chess
{
	public class PlayerTypeSelector : HBoxContainer
	{
		public enum PlayerType
		{
			Human,
			Computer,
			Random
		}

		[Export]
		public string Who;

		[Export]
		public PlayerType Default;

		private OptionButton Selector;

		public PlayerType Selection => (PlayerType)Selector.Selected;

		public override void _Ready()
		{
			var who = GetNode<Label>("Who");
			who.Text = $"{Who}: ";

			Selector = GetNode<OptionButton>("Type");
			Selector.Selected = (int)Default;
		}
	}
}
