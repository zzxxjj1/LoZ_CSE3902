using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class BombDestroyDoor : ICommand
	{
		private Door door;
		private Direction side;

		public BombDestroyDoor(BombPlaced bomb, Door door, Direction side)
		{
			this.door = door;
			this.side = side;
		}

		public void Execute()
		{
			door.Destruct();
		}
	}
}
