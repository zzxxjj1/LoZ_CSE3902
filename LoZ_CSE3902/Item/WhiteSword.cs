using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class WhiteSword : StaticItem
    {
        public WhiteSword(LinkPlayer player, Vector2 pos) : base(player, pos)
        {
            sprite = ItemSpriteFactory.Instance.CreateWhiteSwordSprite();
            this.pos = GetCenteredPos(pos, HitBox.Item.WhiteSword);
        }

        public override void Pickup()
        {
            SoundManager.Instance.Play(SoundEnum.Get_Item);
            isPicked = true;
            //switch link's sword to a more powerful one.
        }

        public override Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.Item.WhiteSword);
        }
    }
}