using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace LoZ_CSE3902
{
	class KeyboardController : IController
	{
		private List<Keys> formerState;
		private List<Keys> currentState;
		private KeyMapping keyMap;

		public KeyboardController(Game1 game)
		{
			formerState = new List<Keys>(Keyboard.GetState().GetPressedKeys());
			currentState = new List<Keys>(Keyboard.GetState().GetPressedKeys());
			keyMap = new KeyMapping(game);
		}

		public void Update()
		{
			formerState = currentState;
			currentState = new List<Keys>(Keyboard.GetState().GetPressedKeys());
			keyMap.ExecuteCommand(formerState, currentState);
		}

		public void SetCommand(GameStateEnum _state)
        {
			keyMap.SetCommand(_state);
        }
	}
}
