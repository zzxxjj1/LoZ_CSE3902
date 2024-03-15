using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class CloneTakeDamage : ICommand
	{
		private LinkClone player;

		public CloneTakeDamage(ICollider player, ICollider npc, Direction side)
		{
			this.player = (LinkClone)player;
		}
		

		public void Execute()
		{
			player.TakeDamage();
		}
	}
}
