using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Heart : StaticItem
    {
        private readonly int heartsToHeal = 2; // A full heart is 2 pieces.
        public Heart(LinkPlayer player, Vector2 pos) : base(player, pos)
        {
            sprite = ItemSpriteFactory.Instance.CreateHeartSprite();
            this.pos = GetCenteredPos(pos, HitBox.Item.Heart);
        }

        public override void Pickup()
        {
            SoundManager.Instance.Play(SoundEnum.Get_Heart);
            isPicked = true;
            player.Healing(heartsToHeal);
        }

        public override Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.Item.Heart);
        }
    }
}