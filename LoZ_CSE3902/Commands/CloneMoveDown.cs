using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class CloneMoveDown : ICommand
	{
		private LinkClone player;

		public CloneMoveDown(LinkClone player)
		{
			this.player = player;
		}

		public void Execute()
		{
			player.MoveDown();
		}
	}
}
