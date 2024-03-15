using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class CloseInventoryMenu : ICommand
	{
		private Game1 myGame;

		public CloseInventoryMenu(Game1 game)
		{
			myGame = game;
		}

		public void Execute()
		{
			if (myGame.gameState is InventoryMenuState)
			{
				InventoryMenuState state = (InventoryMenuState)myGame.gameState;
				if (state.isAnimationStopped) state.StartReturnToGamePlay();
			} else
            {
				throw new InvalidOperationException(
					"CloseInventoryMenu: require SelectionMenuState.");
            }
		}
	}
}
