using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Arrow : StaticItem
    {
        public Arrow(LinkPlayer player, Vector2 pos) : base(player, pos)
        {
            sprite = ItemSpriteFactory.Instance.CreateArrowSprite();
            this.pos = GetCenteredPos(pos, HitBox.Item.Arrow);
        }

        public override void Pickup()
        {
            SoundManager.Instance.Play(SoundEnum.Get_Item);
            isPicked = true;
            // if link don't have available bow&arrow
            if (!player.inventory.GadgetAvailableSet.Contains(GadgetForLink.Bow_Arrow))
                player.inventory.GadgetAvailableSet.Add(GadgetForLink.Arrow);
            if (player.inventory.GadgetAvailableSet.Contains(GadgetForLink.Bow))
            {
                // combine bow&arrow
                player.inventory.GadgetAvailableSet.Remove(GadgetForLink.Arrow);
                player.inventory.GadgetAvailableSet.Remove(GadgetForLink.Bow);
                player.inventory.GadgetAvailableSet.Add(GadgetForLink.Bow_Arrow);
            }
        }

        public override Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.Item.Arrow);
        }
    }
}
