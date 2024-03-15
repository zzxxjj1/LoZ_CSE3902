using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LoZ_CSE3902
{
    public class CloneStepState : ILinkState
    {
        private LinkClone player;
        private ISprite sprite;
        private int frameToNextCut, invincibleFramesLeft;
        private bool goNextFrame;
        private Direction lastDirection;

        public CloneStepState(LinkClone player)
        {
            this.player = player;
            lastDirection = player.facingDirection;
            frameToNextCut = player.FramesPerStep;
            SetSprite(player.facingDirection);
        }

        private void SetSprite(Direction direction)
        {
            switch (direction)
            {
                case (Direction.Down):
                    sprite = LinkSpriteFactory.Instance.CreateCloneStepFrontSprite();
                    break;
                case (Direction.Up):
                    sprite = LinkSpriteFactory.Instance.CreateCloneStepBackSprite();
                    break;
                case (Direction.Left):
                    sprite = LinkSpriteFactory.Instance.CreateCloneStepLeftSprite();
                    break;
                case (Direction.Right):
                    sprite = LinkSpriteFactory.Instance.CreateCloneStepRightSprite();
                    break;
                default: break;
            }
        }
        public void Update()
        {
            if (invincibleFramesLeft > 0)
                invincibleFramesLeft--;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, player.GetPos(), goNextFrame);
            goNextFrame = false;
        }

        // manual control of goNextFrame
        private void UpdateNextFrameFlag()
        {
            frameToNextCut--;
            goNextFrame = frameToNextCut < 0;
            if (goNextFrame) frameToNextCut = player.FramesPerStep;
        }

        public void GridLineUp(Direction side)
        {
            var pos = player.GetPos().ToPoint();
            int remainder;
            switch (side)
            {
                case Direction.Up:
                case Direction.Down:
                    remainder = pos.X % (GameAttributes.Window.TileWidth / 2);
                    if (remainder <= 4) pos.X -= remainder;
                    else pos.X += GameAttributes.Window.TileWidth / 2 - remainder;
                    break;
                case Direction.Left:
                case Direction.Right:
                    remainder = pos.Y % (GameAttributes.Window.TileHeight / 2);
                    if (remainder <= 4) pos.Y -= remainder;
                    else pos.Y += GameAttributes.Window.TileHeight / 2 - remainder;
                    break;
            }
            player.SetPos(pos.ToVector2());
        }

        private void ChangeFacingDireciton(Direction side)
        {
            player.facingDirection = side;
            if (!lastDirection.Equals(side))
            {
                SetSprite(side);
                //GridLineUp(side);
            }
            lastDirection = side;
        }

        public void MoveUp()
        {
            // check if it is necessary to change sprite
            ChangeFacingDireciton(Direction.Up);

            var pos = player.GetPos();
            pos.Y -= player.WalkSpeed;
            player.SetPos(pos);
            UpdateNextFrameFlag();
        }
        public void MoveDown()
        {
            ChangeFacingDireciton(Direction.Down);

            var pos = player.GetPos();
            pos.Y += player.WalkSpeed;
            player.SetPos(pos);
            UpdateNextFrameFlag();
        }
        public void MoveLeft()
        {
            ChangeFacingDireciton(Direction.Left);

            var pos = player.GetPos();
            pos.X -= player.WalkSpeed;
            player.SetPos(pos);
            UpdateNextFrameFlag();

        }
        public void MoveRight()
        {
            ChangeFacingDireciton(Direction.Right);

            var pos = player.GetPos();
            pos.X += player.WalkSpeed;
            player.SetPos(pos);
            UpdateNextFrameFlag();
        }
        public void UseItemA()
        {
            player.state = new CloneSwingSwordState(player);
        }
        public void UseItemB()
        {
            // the clone cannot use gadget
        }
        public void PickUp()
        {

        }
        public void TakeDamage()
        {
            if (invincibleFramesLeft <= 0)
                player.state = new CloneDamagedState(player);
        }
        public void SetInvincibleFrames(int frames)
        {
            invincibleFramesLeft = frames;
        }
    }
}

