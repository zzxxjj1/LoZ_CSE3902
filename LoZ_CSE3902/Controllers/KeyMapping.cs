using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace LoZ_CSE3902
{
    class KeyMapping
    {
        private Game1 myGame;
        private Dictionary<Keys, ICommand> mappings;
        private List<Keys> keysToMoveP1, keysToMoveP2, p1MoveAction, p2MoveAction;

        public KeyMapping(Game1 game)
        {
            myGame = game;
            SetCommand(GameStateEnum.TitleScreen);
        }

        public void SetCommand(GameStateEnum _state)
        {
            switch (_state)
            {
                case GameStateEnum.TitleScreen:
                    SetTitleMenuCommand();
                    break;
                case GameStateEnum.GamePlay:
                    SetGamePlayCommand();
                    break;
                case GameStateEnum.RoomTransition:
                    SetRoomTransitionCommand();
                    break;
                case GameStateEnum.InventoryMenu:
                    SetInventoryMenuCommand();
                    break;
                case GameStateEnum.GameOver:
                    SetGameOverCommand();
                    break;
                case GameStateEnum.Winning:
                    SetGameOverCommand();
                    break;
                case GameStateEnum.GamePlay_2P:
                    SetDuoPlayerCommand();
                    break;
                default:
                    throw new InvalidOperationException("SetCommand: could not find GameStateEnum. (KeyMapping)");
            }
        }
        private void SetUniversalCommand()
        {
            // Reset Mapping
            mappings = new Dictionary<Keys, ICommand>();
            keysToMoveP1 = new List<Keys>();
            keysToMoveP2 = new List<Keys>();
            p1MoveAction = new List<Keys>();
            p2MoveAction = new List<Keys>();

            //Game Commands
            mappings.Add(Keys.Escape, new GoExitProcess(myGame));
            mappings.Add(Keys.R, new ResetGame(myGame));

            //Debug Commands
            mappings.Add(Keys.H, new ToggleHitBox());
            mappings.Add(Keys.J, new DebugPrint(myGame));
        }

        private void SetTitleMenuCommand()
        {
            SetUniversalCommand();
            mappings.Add(Keys.Space, new StartGame(myGame));
        }

        private void SetGamePlayCommand()
        {
            SetUniversalCommand();
            GamePlayState state = (GamePlayState)myGame.gameState;
            LinkPlayer myPlayer = state.player;

            mappings.Add(Keys.Y, new ActivatePlayer2(myGame));

            //Player Commands
            mappings.Add(Keys.W, new PlayerMoveUp(myPlayer));
            mappings.Add(Keys.S, new PlayerMoveDown(myPlayer));
            mappings.Add(Keys.A, new PlayerMoveLeft(myPlayer));
            mappings.Add(Keys.D, new PlayerMoveRight(myPlayer));

            mappings.Add(Keys.Up, new PlayerMoveUp(myPlayer));
            mappings.Add(Keys.Down, new PlayerMoveDown(myPlayer));
            mappings.Add(Keys.Left, new PlayerMoveLeft(myPlayer));
            mappings.Add(Keys.Right, new PlayerMoveRight(myPlayer));

            keysToMoveP1.Add(Keys.W);
            keysToMoveP1.Add(Keys.S);
            keysToMoveP1.Add(Keys.A);
            keysToMoveP1.Add(Keys.D);
            keysToMoveP1.Add(Keys.Up);
            keysToMoveP1.Add(Keys.Down);
            keysToMoveP1.Add(Keys.Left);
            keysToMoveP1.Add(Keys.Right);

            mappings.Add(Keys.Z, new PlayerUseItemA(myPlayer));
            mappings.Add(Keys.N, new PlayerUseItemA(myPlayer));
            mappings.Add(Keys.X, new PlayerUseItemB(myPlayer));
            mappings.Add(Keys.M, new PlayerUseItemB(myPlayer));


            mappings.Add(Keys.D1, new SetGadget1(myPlayer));
            mappings.Add(Keys.NumPad1, new SetGadget1(myPlayer));
            mappings.Add(Keys.D2, new SetGadget2(myPlayer));
            mappings.Add(Keys.NumPad2, new SetGadget2(myPlayer));


            //Menu Commands
            mappings.Add(Keys.Enter, new OpenInventoryMenu(myGame));
            mappings.Add(Keys.Tab, new OpenInventoryMenu(myGame));
            mappings.Add(Keys.P, new PauseGamePlay(state));
        }
        private void SetDuoPlayerCommand()
        {
            SetUniversalCommand();
            GamePlayState state = (GamePlayState)myGame.gameState;
            LinkPlayer myPlayer = state.player;
            LinkClone clone = state.clone;

            mappings.Add(Keys.Y, new DeactivatePlayer2(myGame));

            //Player Commands
            mappings.Add(Keys.W, new PlayerMoveUp(myPlayer));
            mappings.Add(Keys.S, new PlayerMoveDown(myPlayer));
            mappings.Add(Keys.A, new PlayerMoveLeft(myPlayer));
            mappings.Add(Keys.D, new PlayerMoveRight(myPlayer));

            mappings.Add(Keys.N, new PlayerUseItemA(myPlayer));
            mappings.Add(Keys.M, new PlayerUseItemB(myPlayer));

            mappings.Add(Keys.D1, new SetGadget1(myPlayer));
            mappings.Add(Keys.D2, new SetGadget2(myPlayer));


            mappings.Add(Keys.Up, new CloneMoveUp(clone));
            mappings.Add(Keys.Down, new CloneMoveDown(clone));
            mappings.Add(Keys.Left, new CloneMoveLeft(clone));
            mappings.Add(Keys.Right, new CloneMoveRight(clone));
            mappings.Add(Keys.NumPad0, new CloneUseItemA(clone));
            mappings.Add(Keys.RightControl, new CloneUseItemA(clone));

            keysToMoveP1.Add(Keys.W);
            keysToMoveP1.Add(Keys.S);
            keysToMoveP1.Add(Keys.A);
            keysToMoveP1.Add(Keys.D);
            keysToMoveP2.Add(Keys.Up);
            keysToMoveP2.Add(Keys.Down);
            keysToMoveP2.Add(Keys.Left);
            keysToMoveP2.Add(Keys.Right);

            //Menu Commands
            mappings.Add(Keys.Enter, new OpenInventoryMenu(myGame));
            mappings.Add(Keys.Tab, new OpenInventoryMenu(myGame));
            mappings.Add(Keys.P, new PauseGamePlay(state));
        }
        private void SetRoomTransitionCommand()
        {
            SetUniversalCommand();
            //mappings.Add(Keys.Enter, new SkipTransition(myGame));
        }
        private void SetInventoryMenuCommand()
        {
            GamePlayState state = ((InventoryMenuState)myGame.gameState).GamePlay;
            LinkPlayer myPlayer = state.player;

            SetUniversalCommand();

            mappings.Add(Keys.Enter, new CloseInventoryMenu(myGame));
            mappings.Add(Keys.Tab, new CloseInventoryMenu(myGame));

            mappings.Add(Keys.D1, new SetGadget1(myPlayer));
            mappings.Add(Keys.NumPad1, new SetGadget1(myPlayer));
            mappings.Add(Keys.D2, new SetGadget2(myPlayer));
            mappings.Add(Keys.NumPad2, new SetGadget2(myPlayer));

        }
        private void SetGameOverCommand()
        {
            SetUniversalCommand();
        }
        private void SetWinningCommand()
        {
            SetUniversalCommand();
        }
        public void ExecuteCommand(List<Keys> previousKeys, List<Keys> justPressedKeys)
        {
            foreach (Keys k in justPressedKeys)
            {
                // check pressed keys
                if (justPressedKeys.Contains(k) && !previousKeys.Contains(k))
                {
                    if (mappings.ContainsKey(k))
                        mappings[k].Execute();
                    if (keysToMoveP1.Contains(k))
                        p1MoveAction.Add(k);
                    if (keysToMoveP2.Contains(k))
                        p2MoveAction.Add(k);
                }
            }

            foreach (Keys k in previousKeys)
            {
                // check released keys
                if (!justPressedKeys.Contains(k) && previousKeys.Contains(k))
                {
                    p1MoveAction.Remove(k);
                    p2MoveAction.Remove(k);
                }
            }

            if (p1MoveAction.Count > 0)
            {
                mappings[p1MoveAction[p1MoveAction.Count - 1]].Execute();
            }
            if (p2MoveAction.Count > 0)
            {
                mappings[p2MoveAction[p2MoveAction.Count - 1]].Execute();
            }
        }
    }
}
