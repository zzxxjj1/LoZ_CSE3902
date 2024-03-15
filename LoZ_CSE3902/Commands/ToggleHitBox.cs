using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class ToggleHitBox : ICommand
	{
		public ToggleHitBox()
		{
		}

		public void Execute()
		{
			HitBox.showBoarder = !HitBox.showBoarder;
		}
	}
}
