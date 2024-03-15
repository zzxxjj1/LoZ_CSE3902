using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LoZ_CSE3902
{
    public class GamepadController : IController
    {
        private Game1 myGame;

        public GamepadController(Game1 game)
        {
            myGame = game;
        }

        public void Update()
        {
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);
         
            if (capabilities.IsConnected)
            {
                GamePadState currentState = GamePad.GetState(PlayerIndex.One);

                if (myGame.gameState is GamePlayState)
                {
                    GamePlayState state = (GamePlayState)myGame.gameState;

                    if (currentState.IsButtonDown(Buttons.Back))
                    {
                        // pause the game
                    }

                    if (currentState.IsButtonDown(Buttons.Start))
                    {
                        // show in-game menu
                    }

                    if (currentState.IsButtonDown(Buttons.DPadUp) || currentState.ThumbSticks.Left.Y > 0.5f)
                    {
                        new PlayerMoveUp(state.player).Execute();
                    }
                    if (currentState.IsButtonDown(Buttons.DPadDown) || currentState.ThumbSticks.Left.Y < -0.5f)
                    {
                        new PlayerMoveDown(state.player).Execute();
                    }
                    if (currentState.IsButtonDown(Buttons.DPadLeft) || currentState.ThumbSticks.Left.X < -0.5f)
                    {
                        new PlayerMoveLeft(state.player).Execute();
                    }
                    if (currentState.IsButtonDown(Buttons.DPadRight) || currentState.ThumbSticks.Left.X > 0.5f)
                    {
                        new PlayerMoveRight(state.player).Execute();
                    }

                    if (currentState.IsButtonDown(Buttons.A))
                    {
                        new PlayerUseItemA(state.player).Execute();
                    }
                    if (currentState.IsButtonDown(Buttons.B))
                    {
                        new PlayerUseItemB(state.player).Execute();
                    }

                    if (currentState.IsButtonDown(Buttons.X))
                    {
                        new SetGadget1(state.player).Execute();
                    }
                    if (currentState.IsButtonDown(Buttons.Y))
                    {
                        new SetGadget2(state.player).Execute();
                    }
                }
            }
        }

        public void SetCommand(GameStateEnum _state)
        {
            return;
        }
    }
}
