using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class ActivatePlayer2 : ICommand
	{
		private Game1 myGame;

		public ActivatePlayer2(Game1 game)
		{
			myGame = game;
		}

		public void Execute()
		{
			GamePlayState state = (GamePlayState)myGame.gameState;
			state.ActivateLinkClone();
		}
	}
}
