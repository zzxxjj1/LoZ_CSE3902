using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Clock : StaticItem
    {
        public Clock(LinkPlayer player, Vector2 pos) : base(player, pos)
        {
            sprite = ItemSpriteFactory.Instance.CreateClockSprite();
            this.pos = GetCenteredPos(pos, HitBox.Item.Clock);
        }

        public override void Pickup()
        {
            isPicked = true;
            SoundManager.Instance.Play(SoundEnum.Get_Item);
            player.state.SetInvincibleFrames(60 * 10);
        }

        public override Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.Item.Clock);
        }
    }
}