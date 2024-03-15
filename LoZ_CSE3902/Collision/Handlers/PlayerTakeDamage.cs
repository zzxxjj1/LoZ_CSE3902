using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class PlayerTakeDamage : ICommand
	{
		private LinkPlayer player;

		public PlayerTakeDamage(LinkPlayer player)
		{
			this.player = player;
		}

		public PlayerTakeDamage(ICollider player, ICollider npc, Direction side)
		{
			this.player = (LinkPlayer)player;
		}
		

		public void Execute()
		{
			player.TakeDamage();
		}
	}
}
