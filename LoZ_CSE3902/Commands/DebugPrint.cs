using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class DebugPrint : ICommand
	{
		private Game1 game;

		public DebugPrint(Game1 game)
		{
			this.game = game;
		}

		public void Execute()
		{

			Debug.Print("-------DebugPrint-------");
			Debug.Print("Current GameState: {0}", game.gameState);

			if (game.gameState is GamePlayState)
            {
				GamePlayState state = (GamePlayState)game.gameState;
				Debug.Print("Level Name: {0}", state.mapping.Name);
				Debug.Print("In Room {0}, Level {0}", state.mapping.CurrentRoom, state.mapping.Level);
				Debug.Print("Link Pos: {0}", state.player.GetPos());

				state.player.inventory.rupeeCount += 5;
				state.player.inventory.keyCount += 5;
				state.player.inventory.bombCount += 5;
				state.player.inventory.CheckBagCapacity();

				state.player.Healing(1);
				state.player.IncreaseHeartLimit(2);

				Debug.Print("Rupee {0}, Key {1}, Bomb {2}",
					state.player.inventory.rupeeCount,
					state.player.inventory.keyCount,
					state.player.inventory.bombCount);
				Debug.Print("Health {0}, Max {1}",
					state.player.health, state.player.maxHealth);
			}
			
		}
	}
}
