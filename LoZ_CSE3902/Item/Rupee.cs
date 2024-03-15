using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LoZ_CSE3902
{
    public class Rupee : StaticItem
    {
        public Rupee(LinkPlayer player, Vector2 pos) : base(player, pos)
        {
            sprite = ItemSpriteFactory.Instance.CreateRupeeSprite();
            this.pos = GetCenteredPos(pos, HitBox.Item.Rupee);
        }
        public override void Pickup()
        {
            SoundManager.Instance.Play(SoundEnum.Get_Rupee);
            isPicked = true;
            player.inventory.rupeeCount += 5;
            player.inventory.CheckBagCapacity();
        }

        public override Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.Item.Rupee);
        }
    }
}