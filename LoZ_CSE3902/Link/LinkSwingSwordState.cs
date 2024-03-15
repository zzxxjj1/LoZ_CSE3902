using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LoZ_CSE3902
{
    public class LinkSwingSwordState : ILinkState
    {
        private LinkPlayer player;
        private ISprite sprite;
        private int framesLeft;
        private Direction side;

        public LinkSwingSwordState(LinkPlayer player)
        {
            this.player = player;
            framesLeft = player.FramesToSwingSword;
            side = player.facingDirection;
            SoundManager.Instance.Play(SoundEnum.Arrow_Boomerang);
            SetSprite(player.facingDirection);

            player.inventory.TryShootSword();
        }

        private void SetSprite(Direction direction)
        {
            switch (direction)
            {
                case (Direction.Down):
                    sprite = LinkSpriteFactory.Instance.CreateLinkSwingSwordFrontSprite();
                    break;
                case (Direction.Up):
                    sprite = LinkSpriteFactory.Instance.CreateLinkSwingSwordBackSprite();
                    break;
                case (Direction.Left):
                    sprite = LinkSpriteFactory.Instance.CreateLinkSwingSwordLeftSprite();
                    break;
                case (Direction.Right):
                    sprite = LinkSpriteFactory.Instance.CreateLinkSwingSwordRightSprite();
                    break;
                default: break;
            }
        }
        public void Update()
        {
            framesLeft--;
            if (framesLeft <= 0)
            {
                player.state = new LinkStepState(player);
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
            player.state = new LinkDamagedState(player);
        }
        public void SetInvincibleFrames(int frames)
        {

        }
    }
}

