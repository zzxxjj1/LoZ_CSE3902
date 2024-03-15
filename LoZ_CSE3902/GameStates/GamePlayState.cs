using LoZ_CSE3902;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace LoZ_CSE3902
{
    public class GamePlayState : IGameState
    {
        public static Game1 game;
        public static HUDBar hudBar;

        public CollisionManager collisionManager;
        public LevelMapping mapping;
        public Room room;
        public LinkPlayer player;
        public LinkClone clone;

        public bool isDuoPlay = false;
        
        public const int StartLevel = 1;

        public GamePlayState(Game1 game)
        {
            GamePlayState.game = game;
            SoundManager.Instance.SetBGM(SoundEnum.BGM_Underworld);

            player = new LinkPlayer(game);
            mapping = LevelMapping.Create(StartLevel);
            mapping.CurrentRoom = mapping.StartRoom;
            mapping.InitializeAllRooms(game, player);
            room = mapping.GetCurrentRoom();

            collisionManager = new CollisionManager(game);
            collisionManager.SetDetector(room, player);

            hudBar = new HUDBar(player);
            hudBar.SetMiniMap(StartLevel);
            hudBar.SetCurrentRoom(mapping.Size, 
            mapping.GetLayoutPositionFromID(mapping.CurrentRoom));
        }

        public void Update()
        {
            foreach (IController controller in game.controllerList)
            {
                controller.Update();
            }

            if (!GameAttributes.Paused)
            {
                collisionManager.Update();

                player.Update();
                room.Update();
                hudBar.Update();

                UpdateClone();
            }

            CheckGameOver();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            GameUtility.Instance.GamePlayDrawingBegin();
            room.Draw(spriteBatch);
            player.Draw(spriteBatch);
            DrawClone(spriteBatch);
            GameUtility.Instance.SpriteBatch.End();

            GameUtility.Instance.HUDDrawingBegin();
            GameUtility.Instance.GamePlayDrawingBegin(false);
            hudBar.Draw();
            GameUtility.Instance.SpriteBatch.End();
            GameUtility.Instance.SpriteBatchHUD.End();
        }

        public void CommandSetUp()
        {
            if (isDuoPlay)
            {
                foreach (IController controller in game.controllerList)
                {
                    controller.SetCommand(GameStateEnum.GamePlay_2P);
                }
            }
            else
            {
                foreach (IController controller in game.controllerList)
                {
                    controller.SetCommand(GameStateEnum.GamePlay);
                }
            }
        }

        public void ChangeRoom(Direction side)
        {
            if (room.IsDoorOpen(side))
            {
                //TeleportRoom(room.data.ID, side);
                mapping.SaveRoom(room);
                game.gameState = new RoomTransitionState(this, side);
                game.gameState.CommandSetUp();
            }
        }

        private void CheckGameOver()
        {
            if (player.health <= 0)
            {
                game.gameState = new GameOverState(game);
                game.gameState.CommandSetUp();
            }
        }

        public void ActivateLinkClone()
        {
            isDuoPlay = true;
            clone = new LinkClone(game, player);
            CommandSetUp();
        }
        public void DeactivateLinkClone()
        {
            isDuoPlay = false;
            clone = null;
            CommandSetUp();
        }
        public void UpdateClone()
        {
            if (clone != null)
            {
                clone.Update();
            }
        }
        public void DrawClone(SpriteBatch spriteBatch)
        {
            if (clone != null)
            {
                clone.Draw(spriteBatch);
            }
        }
    }
}
