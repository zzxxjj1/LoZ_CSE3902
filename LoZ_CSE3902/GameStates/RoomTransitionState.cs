using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace LoZ_CSE3902
{
    class RoomTransitionState : IGameState
    {
        private Game1 game;
        private GamePlayState gameplay;
        private LevelMapping mapping;
        private LinkPlayer player;

        private Room current, target;
        private Direction side;
        private Vector2 targetInitialOffset;

        private int inGameHeight = GameAttributes.Window.InGameHeight;
        private int inGameWidth = GameAttributes.Window.InGameWidth;

        private int framesLeft;
        private float totalFrames;
        private float transitionSpeed = GameAttributes.Room.TransitionSpeed; // px per frame
        private Vector2 offsetPerFrame;

        public RoomTransitionState(GamePlayState gameplay, Direction side)
        {
            this.gameplay = gameplay;
            this.game = GamePlayState.game;
            this.mapping = gameplay.mapping;
            this.player = gameplay.player;
            this.side = side;

            current = mapping.GetCurrentRoom();
            target = mapping.GetNextRoom(side);

            CalculateOffset(side);
            SetTargetInitialPosition(side);
        }
        public void CommandSetUp()
        {
            foreach (IController controller in game.controllerList)
            {
                controller.SetCommand(GameStateEnum.RoomTransition);
            }
        }
        public void Update()
        {
            foreach (IController controller in game.controllerList)
            {
                controller.Update();
            }

            if (--framesLeft <= 0)
            {
                BackToGamePlay();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 offset = offsetPerFrame * (totalFrames - framesLeft);

            GameUtility.Instance.GamePlayDrawingBegin(offset);
            current.DrawForTransition(spriteBatch);
            GameUtility.Instance.SpriteBatch.End();

            GameUtility.Instance.GamePlayDrawingBegin(targetInitialOffset + offset);
            target.DrawForTransition(spriteBatch);
            GameUtility.Instance.SpriteBatch.End();

            GameUtility.Instance.HUDDrawingBegin();
            GameUtility.Instance.GamePlayDrawingBegin(false);
            GamePlayState.hudBar.Draw();
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
                    totalFrames = inGameHeight / transitionSpeed;
                    offsetPerFrame = new Vector2(0, transitionSpeed);
                    break;
                case Direction.Left:
                    totalFrames = inGameWidth / transitionSpeed;
                    offsetPerFrame = new Vector2(transitionSpeed, 0);
                    break;
                case Direction.Right:
                    totalFrames = inGameWidth / transitionSpeed;
                    offsetPerFrame = new Vector2(-transitionSpeed, 0);
                    break;
                case Direction.Down:
                    totalFrames = inGameHeight / transitionSpeed;
                    offsetPerFrame = new Vector2(0, -transitionSpeed);
                    break;
                default:
                    throw new InvalidOperationException(
                        "CalculateOffset: Check if side is valid.");
            }
            framesLeft = (int)totalFrames;
        }

        private void BackToGamePlay()
        {
            mapping.SwitchRoomBySide(side);
            SetPlayerPositionAfterTransition();
            if (gameplay.isDuoPlay) SetClonePositionAfterTransition();

            gameplay.room = mapping.GetCurrentRoom();
            gameplay.collisionManager.SetDetector(gameplay.room, player);
            game.gameState = gameplay;
            game.gameState.CommandSetUp();
            GamePlayState.hudBar.SetCurrentRoom(mapping.Size,
                mapping.GetLayoutPositionFromID(mapping.CurrentRoom));

            Debug.Print("BackToGamePlay: Go to Room {0}, Side {1}",
                target.data.ID, side.ToString());
        }

        private void SetPlayerPositionAfterTransition()
        {
            Vector2 playerPosition = player.GetPos();
            //Set position depend on pos in previous room
            switch (side)
            {
                case Direction.Up:
                    player.SetPos(
                        Room.ConvertGridToScreenPosition(
                            GameAttributes.Player.Pos.AfterRoomTransit[0],
                            target.data.InUnderworld));
                    break;
                case Direction.Left:
                    player.SetPos(
                        Room.ConvertGridToScreenPosition(
                            GameAttributes.Player.Pos.AfterRoomTransit[1],
                            target.data.InUnderworld));
                    break;
                case Direction.Right:
                    player.SetPos(
                        Room.ConvertGridToScreenPosition(
                            GameAttributes.Player.Pos.AfterRoomTransit[2],
                            target.data.InUnderworld));
                    break;
                case Direction.Down:
                    player.SetPos(
                        Room.ConvertGridToScreenPosition(
                            GameAttributes.Player.Pos.AfterRoomTransit[3],
                            target.data.InUnderworld));
                    break;
            }
        }
        private void SetClonePositionAfterTransition()
        {
            Vector2 playerPosition = gameplay.clone.GetPos();
            switch (side)
            {
                case Direction.Up:
                    gameplay.clone.SetPos(
                        Room.ConvertGridToScreenPosition(
                            GameAttributes.Player.Pos.AfterRoomTransit[0],
                            target.data.InUnderworld));
                    break;
                case Direction.Left:
                    gameplay.clone.SetPos(
                        Room.ConvertGridToScreenPosition(
                            GameAttributes.Player.Pos.AfterRoomTransit[1],
                            target.data.InUnderworld));
                    break;
                case Direction.Right:
                    gameplay.clone.SetPos(
                        Room.ConvertGridToScreenPosition(
                            GameAttributes.Player.Pos.AfterRoomTransit[2],
                            target.data.InUnderworld));
                    break;
                case Direction.Down:
                    gameplay.clone.SetPos(
                        Room.ConvertGridToScreenPosition(
                            GameAttributes.Player.Pos.AfterRoomTransit[3],
                            target.data.InUnderworld));
                    break;
            }
        }
        public void SetTargetInitialPosition(Direction side)
        {
            switch (side)
            {
                case Direction.Up:
                    targetInitialOffset = new Vector2(0, -GameAttributes.Window.InGameHeight);
                    break;
                case Direction.Left:
                    targetInitialOffset = new Vector2(-GameAttributes.Window.InGameWidth, 0);
                    break;
                case Direction.Right:
                    targetInitialOffset = new Vector2(GameAttributes.Window.InGameWidth, 0);
                    break;
                case Direction.Down:
                    targetInitialOffset = new Vector2(0, GameAttributes.Window.InGameHeight);
                    break;
                default:
                    throw new InvalidOperationException(
                        "CalculateOffset: Check if side is valid.");
            }
        }


    }
}

