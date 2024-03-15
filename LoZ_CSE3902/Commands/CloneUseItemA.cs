using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class CloneUseItemA : ICommand
	{
		private LinkClone player;

		public CloneUseItemA(LinkClone player)
		{
			this.player = player;
		}

		public void Execute()
		{
			player.UseItemA();
		}
	}
}

