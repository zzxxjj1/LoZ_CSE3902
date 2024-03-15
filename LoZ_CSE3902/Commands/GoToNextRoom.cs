using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	public class GoToNextRoom : ICommand
	{
		private Game1 game;
		private Direction direction;
		private bool isMouseClick = false;

		public GoToNextRoom(Game1 game)
		{
			this.game = game;
		}
		// an overload for mouse controller
		public GoToNextRoom(Game1 game, Direction direction)
		{
			this.game = game;
			this.direction = direction;
			isMouseClick = true;
		}

		public void Execute()
		{
			if (!(game.gameState is GamePlayState))
				throw new InvalidOperationException("GoToNextRoom: Execution failed. Require GamePlayState.");

			GamePlayState state = (GamePlayState)game.gameState;
			if (isMouseClick)
			{
				if (state.room.data.InUnderworld &&
					!state.room.exterior.door[(int)direction].IsOpen())
				{
					state.room.exterior.door[(int)direction].Unlock(state.player);
					state.room.exterior.door[(int)direction].Destruct();
					state.room.exterior.door[(int)direction].Open();
				}
				else
				{
					state.ChangeRoom(direction);
					state.player.ChangeFacing(direction);
					if (state.isDuoPlay) state.clone.ChangeFacing(direction);
				}
			}
			else
			{
				state.ChangeRoom(state.player.facingDirection);
				state.player.ChangeFacing(state.player.facingDirection);
				if (state.isDuoPlay) state.clone.ChangeFacing(state.player.facingDirection);
			}

			//Debug.Print("GoToNextRoom: Executed.");
		}
	}
}
