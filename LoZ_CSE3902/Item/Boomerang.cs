using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Boomerang : StaticItem
    {
        public Boomerang(LinkPlayer player, Vector2 pos) : base(player, pos)
        {
            sprite = ItemSpriteFactory.Instance.CreateBoomerangSprite();
            this.pos = GetCenteredPos(pos, HitBox.Item.Boomerang);
        }

        public override void Pickup()
        {
            SoundManager.Instance.Play(SoundEnum.Get_Item);
            isPicked = true;
            player.inventory.GadgetAvailableSet.Add(GadgetForLink.Boomerang);
        }

        public override Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.Item.Boomerang);
        }
    }
}