using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class OpenInventoryMenu : ICommand
	{
		private Game1 myGame;

		public OpenInventoryMenu(Game1 game)
		{
			myGame = game;
		}

		public void Execute()
		{
			if (myGame.gameState is GamePlayState)
			{
				GamePlayState state = (GamePlayState)myGame.gameState;
				myGame.gameState = new InventoryMenuState(state);
				myGame.gameState.CommandSetUp();
			} else
            {
				throw new InvalidOperationException(
					"OpenInventoryMenu: require GamePlayState.");
            }
		}
	}
}
