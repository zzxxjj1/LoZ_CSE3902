using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class NPCTakeDamage : ICommand
	{
		private INPC npc;
		private Direction side;

		public NPCTakeDamage(INPC npc)
		{
			this.npc = npc;
		}

		public NPCTakeDamage(INPC npc, IGadgetEffect gadget, Direction side)
		{
			this.npc = npc;
			this.side = side;
		}

		public void Execute()
		{
			npc.TakeDamage(side);
		}
	}
}
