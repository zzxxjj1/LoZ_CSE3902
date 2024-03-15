using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LoZ_CSE3902
{
    public class LinkUseItemState : ILinkState
    {
        private LinkPlayer player;
        private ISprite sprite;
        private int framesLeft;

        public LinkUseItemState(LinkPlayer player)
        {
            this.player = player;
            SetSprite(player.facingDirection);
            framesLeft = player.FramesToUseItem;

            player.inventory.UseGadget();
        }

        private void SetSprite(Direction direction)
        {
            switch (direction)
            {
                case (Direction.Down):
                    sprite = LinkSpriteFactory.Instance.CreateLinkUseItemFrontSprite();
                    break;
                case (Direction.Up):
                    sprite = LinkSpriteFactory.Instance.CreateLinkUseItemBackSprite();
                    break;
                case (Direction.Left):
                    sprite = LinkSpriteFactory.Instance.CreateLinkUseItemLeftSprite();
                    break;
                case (Direction.Right):
                    sprite = LinkSpriteFactory.Instance.CreateLinkUseItemRightSprite();
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
            sprite.Draw(spriteBatch, player.GetPos());
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
            // player.health--;
        }
        public void SetInvincibleFrames(int frames)
        {

        }
    }
}

