using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace LoZ_CSE3902
{
    class InventoryMenuState : IGameState
    {
        private Game1 game;
        private LevelMapping mapping;
        private LinkPlayer player;
        private GamePlayState gameplay;

        public GamePlayState GamePlay
        {
            get { return gameplay; }
        }

        private Room current;
        private InventoryDisplay inventoryMenu;
        private BigMapDisplay bigMap;

        private const int inventoryHeight = 88;
        private const int mapHeight = 88;
        private const int scrollDistance = inventoryHeight + mapHeight;

        private int framesLeft;
        private float totalFrames;
        private float transitionSpeed = GameAttributes.Room.TransitionSpeed; // px per frame
        private Vector2 offsetPerFrame, HUDPosition, roomPosition;
        public bool isAnimationStopped = false; // a flag to prevent command from excution in animatin
        private bool isReturnProcess = false;

        public InventoryMenuState(GamePlayState gameplay)
        {
            this.game = GamePlayState.game;
            this.gameplay = gameplay;
            this.mapping = gameplay.mapping;
            this.player = gameplay.player;

            inventoryMenu = new InventoryDisplay(gameplay);
            inventoryMenu.UpdateListDisplay(player);
            bigMap = new BigMapDisplay(gameplay);
            current = mapping.GetCurrentRoom();

            CalculateOffset(Direction.Up);
        }
        public void CommandSetUp()
        {
            foreach (IController controller in game.controllerList)
            {
                controller.SetCommand(GameStateEnum.InventoryMenu);
            }
        }
        public void Update()
        {
            foreach (IController controller in game.controllerList)
            {
                controller.Update();
            }

            if (framesLeft > 0 & !isReturnProcess)
            {
                framesLeft--;
                HUDPosition += offsetPerFrame;
                roomPosition += offsetPerFrame;
            } else
                isAnimationStopped = true;
            
            if (isReturnProcess)
            {
                isAnimationStopped = false;
                framesLeft--;
                HUDPosition -= offsetPerFrame;
                roomPosition -= offsetPerFrame;
                if (framesLeft <= 0)
                {
                    BackToGamePlay();
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            GameUtility.Instance.GamePlayDrawingBegin(roomPosition);
            current.DrawForTransition(spriteBatch);
            GameUtility.Instance.SpriteBatch.End();

            GameUtility.Instance.HUDDrawingBegin();
            GameUtility.Instance.GamePlayDrawingBegin(false);
            inventoryMenu.Draw(HUDPosition);
            bigMap.Draw(HUDPosition + new Vector2(0, 88));
            GamePlayState.hudBar.Draw(HUDPosition + new Vector2(0, 176));
            GameUtility.Instance.SpriteBatch.End();
            GameUtility.Instance.SpriteBatchHUD.End();
        }

        public void Reset()
        {
            game.gameState = new TitleState(game);
        }

        private void CalculateOffset(Direction side)
        {
            switch (side)
            {
                case Direction.Up:
                    totalFrames = scrollDistance / transitionSpeed;
                    offsetPerFrame = new Vector2(0, transitionSpeed);
                    break;
                case Direction.Down:
                    totalFrames = scrollDistance / transitionSpeed;
                    offsetPerFrame = new Vector2(0, -transitionSpeed);
                    break;
                default:
                    throw new InvalidOperationException(
                        "CalculateOffset: Check if side is valid.");
            }
            framesLeft = (int)totalFrames;
            HUDPosition = new Vector2(0, -scrollDistance);
            roomPosition = new Vector2(0);
        }

        public void StartReturnToGamePlay()
        {
            isAnimationStopped = false;
            isReturnProcess = true;
            framesLeft = (int)totalFrames - framesLeft;
        }

        public void BackToGamePlay()
        {
            isReturnProcess = false;
            game.gameState = gameplay;
            game.gameState.CommandSetUp();
        }
    }
}

