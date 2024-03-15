using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Key : StaticItem
    {
        public Key(LinkPlayer player, Vector2 pos) : base(player, pos)
        {
            sprite = ItemSpriteFactory.Instance.CreateKeySprite();
            this.pos = GetCenteredPos(pos, HitBox.Item.Key);
        }

        public override void Pickup()
        {
            SoundManager.Instance.Play(SoundEnum.Get_Item);
            isPicked = true;
            player.inventory.keyCount++;
            player.inventory.CheckBagCapacity();
        }

        public override Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.Item.Key);
        }
    }
}
