using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LoZ_CSE3902
{
    public class ArrowShooted : IGadgetEffect
    {
        public LinkPlayer Player
        {
            get { return player; }
        }

        private LinkPlayer player;
        private ISprite sprite;
        public Vector2 pos;
        private Direction direction;
        private Boolean sound;

        private const int ArrowPosOffset = 0, ArrowXOffset = 0;
        private const float ArrowSpeed = 3; // per frame

        // distance check should be replaced by collision check
        public float distance = 200;

        public ArrowShooted(LinkPlayer player)
        {
            this.player = player;
            direction = this.player.facingDirection;
            sound = false;

            pos = this.player.GetPos();
            switch (this.player.facingDirection)
            {
                case Direction.Down:
                    sprite = ItemSpriteFactory.Instance.CreateArrowShootedDownSprite();
                    pos = new Vector2(pos.X + ArrowXOffset,
                        pos.Y + ArrowPosOffset + LinkSpriteFactory.Instance.useItemFrontFrameSize.Height);
                    break;
                case Direction.Up:
                    sprite = ItemSpriteFactory.Instance.CreateArrowShootedUpSprite();
                    pos = new Vector2(pos.X + ArrowXOffset,
                        pos.Y - ArrowPosOffset - ItemSpriteFactory.Instance.ArrowShootedUpFrameSize.Height);
                    break;
                case Direction.Left:
                    sprite = ItemSpriteFactory.Instance.CreateArrowShootedLeftSprite();
                    pos = new Vector2(
                        pos.X - (ArrowPosOffset + ItemSpriteFactory.Instance.ArrowShootedLeftFrameSize.Width),
                        pos.Y);
                    break;
                case Direction.Right:
                    sprite = ItemSpriteFactory.Instance.CreateArrowShootedRightSprite();
                    pos = new Vector2(
                        pos.X + (ArrowPosOffset + LinkSpriteFactory.Instance.useItemRightFrameSize.Width),
                        pos.Y);
                    break;
                default: break;
            }
        }

        public void Update()
        {
            if (!sound)
            {
                sound = true;
                SoundManager.Instance.Play(SoundEnum.Arrow_Boomerang);
            }
            distance -= ArrowSpeed;
            Boolean isStop = distance <= 0; // will handle collision here.
            switch (direction)
            {
                case (Direction.Down):
                    pos.Y += ArrowSpeed;
                    break;
                case (Direction.Up):
                    pos.Y -= ArrowSpeed;
                    break;
                case (Direction.Left):
                    pos.X -= ArrowSpeed;
                    break;
                case (Direction.Right):
                    pos.X += ArrowSpeed;
                    break;
                default: break;
            }
            if (isStop) player.inventory.TerminateGadget();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, pos);

            if (HitBox.showBoarder)
                GameUtility.Instance.DrawBoarder(GetRectangle(), Color.Red, 1);
        }
        public Rectangle GetRectangle()
        {
            if (direction == Direction.Down || direction == Direction.Up)
            {
                return HitBox.Create(pos.ToPoint(), HitBox.Link.Arrow);
            }
            else
            {
                return HitBox.Create(pos.ToPoint(), HitBox.Link.Arrow);
            }
        }
    }
}
