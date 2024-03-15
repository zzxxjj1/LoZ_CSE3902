using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Compass : StaticItem
    {
        public Compass(LinkPlayer player, Vector2 pos) : base(player, pos)
        {
            sprite = ItemSpriteFactory.Instance.CreateCompassSprite();
            this.pos = GetCenteredPos(pos, HitBox.Item.Compass);
        }

        public override void Pickup()
        {
            isPicked = true;
            SoundManager.Instance.Play(SoundEnum.Get_Item);
            GamePlayState.hudBar.ShowDestinationOnMap(true);
        }

        public override Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.Item.Compass);
        }
    }
}
