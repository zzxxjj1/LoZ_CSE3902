using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class SetGadget2 : ICommand
	{
		private LinkPlayer player;

		public SetGadget2(LinkPlayer player)
		{
			this.player = player;
		}

		public void Execute()
		{
			player.SetGadget(GadgetForLink.Bow_Arrow);
		}
	}
}

