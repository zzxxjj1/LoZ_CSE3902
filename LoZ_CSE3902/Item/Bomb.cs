using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Bomb : StaticItem
    {
        public Bomb(LinkPlayer player, Vector2 pos) : base(player, pos)
        {
            sprite = ItemSpriteFactory.Instance.CreateBombSprite();
            this.pos = GetCenteredPos(pos, HitBox.Item.Bomb);
        }

        public override void Pickup()
        {
            SoundManager.Instance.Play(SoundEnum.Get_Item);
            isPicked = true;
            player.inventory.GadgetAvailableSet.Add(GadgetForLink.Bomb);
            player.inventory.bombCount += 5;
            player.inventory.CheckBagCapacity();
        }

        public override Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.Item.Bomb);
        }
    }
}