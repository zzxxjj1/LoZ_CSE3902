using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Map : StaticItem
    {
        public Map(LinkPlayer player, Vector2 pos) : base(player, pos)
        {
            sprite = ItemSpriteFactory.Instance.CreateMapSprite();
            this.pos = GetCenteredPos(pos, HitBox.Item.Map);
        }

        public override void Pickup()
        {
            SoundManager.Instance.Play(SoundEnum.Get_Item);
            isPicked = true;
            GamePlayState.hudBar.RevealMap(true);
        }

        public override Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.Item.Map);
        }
    }
}