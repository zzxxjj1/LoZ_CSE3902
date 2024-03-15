using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class PauseGamePlay : ICommand
	{
		GamePlayState state;
		public PauseGamePlay(GamePlayState state)
		{
			this.state = state;
		}

		public void Execute()
		{
			GameAttributes.Paused = !GameAttributes.Paused;
			SoundManager.Instance.GamePaused();
		}
	}
}
