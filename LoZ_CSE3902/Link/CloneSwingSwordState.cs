using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LoZ_CSE3902
{
    public class CloneSwingSwordState : ILinkState
    {
        private LinkClone player;
        private ISprite sprite;
        private int framesLeft;
        private Direction side;

        public CloneSwingSwordState(LinkClone player)
        {
            this.player = player;
            framesLeft = player.FramesToSwingSword;
            side = player.facingDirection;
            SoundManager.Instance.Play(SoundEnum.Arrow_Boomerang);
            SetSprite(player.facingDirection);
        }

        private void SetSprite(Direction direction)
        {
            switch (direction)
            {
                case (Direction.Down):
                    sprite = LinkSpriteFactory.Instance.CreateCloneSwingSwordFrontSprite();
                    break;
                case (Direction.Up):
                    sprite = LinkSpriteFactory.Instance.CreateCloneSwingSwordBackSprite();
                    break;
                case (Direction.Left):
                    sprite = LinkSpriteFactory.Instance.CreateCloneSwingSwordLeftSprite();
                    break;
                case (Direction.Right):
                    sprite = LinkSpriteFactory.Instance.CreateCloneSwingSwordRightSprite();
                    break;
                default: break;
            }
        }
        public void Update()
        {
            framesLeft--;
            if (framesLeft <= 0)
            {
                player.state = new CloneStepState(player);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 pos = player.GetPos();
            switch (side)
            {
                case Direction.Up:
                    pos.Y = pos.Y - player.LinkHeight + 4.0f;
                    break;
                case Direction.Left:
                    pos.X = pos.X + player.LinkWidth - 27.0f + 1.0f;
                    break;
                default: // no drawing offset
                    break;
            }
            sprite.Draw(spriteBatch, pos);
        }
        public void MoveUp()
        {

        }
        public void MoveDown()
        {

        }
        public void MoveLeft()
        {

        }
        public void MoveRight()
        {

        }
        public void UseItemA()
        {

        }
        public void UseItemB()
        {

        }
        public void PickUp()
        {

        }
        public void TakeDamage()
        {
            player.state = new CloneDamagedState(player);
        }
        public void SetInvincibleFrames(int frames)
        {

        }
    }
}

