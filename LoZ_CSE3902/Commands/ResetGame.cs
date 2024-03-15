using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class ResetGame : ICommand
	{
		private Game1 myGame;

		public ResetGame(Game1 game)
		{
			myGame = game;
		}

		public void Execute()
		{
			myGame.Reset();
		}
	}
}
