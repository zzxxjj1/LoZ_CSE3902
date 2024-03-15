using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class PlayerUseItemB : ICommand
	{
		private LinkPlayer player;

		public PlayerUseItemB(LinkPlayer player)
		{
			this.player = player;
		}

		public void Execute()
		{
			player.UseItemB();
		}
	}
}

