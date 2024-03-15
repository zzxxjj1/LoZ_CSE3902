using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class CloneMoveUp : ICommand
	{
		private LinkClone player;

		public CloneMoveUp(LinkClone player)
		{
			this.player = player;
		}

		public void Execute()
		{
			player.MoveUp();
		}
	}
}
