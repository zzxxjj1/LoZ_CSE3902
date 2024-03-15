using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace LoZ_CSE3902
{
    class MouseController : IController
    {
		private Dictionary<Rectangle, ICommand> controllerMappings, rightClickMappings;
		private MouseState oldState;
		Game1 myGame;

		public MouseController(Game1 game)
		{
			myGame = game;
		}

		public void RegisterCommandLeftClick(int a, int b, int x, int y, ICommand command)
		{
			Rectangle rectangle = new Rectangle(a, b, x, y);
			controllerMappings.Add(rectangle, command);
			oldState = Mouse.GetState();
		}
		public void RegisterCommandRightClick(int a, int b, int x, int y, ICommand command)
		{
			Rectangle rectangle = new Rectangle(a, b, x, y);
			rightClickMappings.Add(rectangle, command);
			oldState = Mouse.GetState();
		}
		public void RegisterCommandLeftClick(Rectangle area, ICommand command)
		{
			controllerMappings.Add(area, command);
			oldState = Mouse.GetState();
		}
		public void RegisterCommandRightClick(Rectangle area, ICommand command)
		{
			rightClickMappings.Add(area, command);
			oldState = Mouse.GetState();
		}

		public void SetCommand(GameStateEnum state)
		{
			controllerMappings = new Dictionary<Rectangle, ICommand>();
			rightClickMappings = new Dictionary<Rectangle, ICommand>();

			int width = myGame.Window.ClientBounds.Width;
			int height = myGame.Window.ClientBounds.Height;

			//RegisterCommandRightClick(0, 0, width, height, new DebugPrint(myGame));

			if (state.Equals(GameStateEnum.TitleScreen))
			{
				RegisterCommandLeftClick(0, 0, width, height, new StartGame(myGame));
			}

			if (state.Equals(GameStateEnum.GamePlay))
            {
				GamePlayState gameplay = (GamePlayState)myGame.gameState;

				RegisterCommandLeftClick(112, 0 + GameAttributes.Window.HUDBarHeight, 32, 32,
					new GoToNextRoom(myGame, Direction.Up));
				RegisterCommandLeftClick(0, 72 + GameAttributes.Window.HUDBarHeight, 32, 32, 
					new GoToNextRoom(myGame, Direction.Left));
				RegisterCommandLeftClick(224, 72 + GameAttributes.Window.HUDBarHeight, 32, 32, 
					new GoToNextRoom(myGame, Direction.Right));
				RegisterCommandLeftClick(112, 144 + GameAttributes.Window.HUDBarHeight, 32, 32, 
					new GoToNextRoom(myGame, Direction.Down));
				RegisterCommandLeftClick(GameAttributes.Room.UnderworldBoundary,
					new PlayerUseItemA(gameplay.player));
				RegisterCommandRightClick(GameAttributes.Room.UnderworldBoundary,
					new PlayerUseItemB(gameplay.player));
			}
			if (state.Equals(GameStateEnum.GamePlay_2P))
			{
				GamePlayState gameplay = (GamePlayState)myGame.gameState;

				RegisterCommandLeftClick(112, 0 + GameAttributes.Window.HUDBarHeight, 32, 32,
					new GoToNextRoom(myGame, Direction.Up));
				RegisterCommandLeftClick(0, 72 + GameAttributes.Window.HUDBarHeight, 32, 32,
					new GoToNextRoom(myGame, Direction.Left));
				RegisterCommandLeftClick(224, 72 + GameAttributes.Window.HUDBarHeight, 32, 32,
					new GoToNextRoom(myGame, Direction.Right));
				RegisterCommandLeftClick(112, 144 + GameAttributes.Window.HUDBarHeight, 32, 32,
					new GoToNextRoom(myGame, Direction.Down));

			}
		}

		public void Update()
		{
			MouseState newState = Mouse.GetState();
			var scaledMousePosition = Vector2.Transform(newState.Position.ToVector2(), 
				Matrix.Invert(GameAttributes.Window.ScalingMatrix));
			var mousePoint = new Point((int)scaledMousePosition.X, (int)scaledMousePosition.Y);
            //Debug.Print("Mouse clicked on ({0}, {1}).", newState.X, newState.Y);
            //Debug.Print("Scaled to ({0}, {1}).", mousePoint.X, mousePoint.Y);

            // Check right click
            if (newState.RightButton == ButtonState.Pressed && oldState.RightButton == ButtonState.Released)
			{
				foreach (KeyValuePair<Rectangle, ICommand> entry in rightClickMappings)
				{
					if (entry.Key.Contains(mousePoint))
					{
						entry.Value.Execute();
					}
				}
			}

			// Check left click
			if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
			{
				foreach (KeyValuePair<Rectangle, ICommand> entry in controllerMappings)
				{
					//Debug.Print("Key Rectangle: {0}", entry.Key.ToString());
					if (entry.Key.Contains(mousePoint))
                    {
						entry.Value.Execute();
                    }
                }
			}

			oldState = newState; // reassigns the old state so that it is ready for next time
		}
	}
}
