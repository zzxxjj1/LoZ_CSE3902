using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class HeartContainer : StaticItem
    {
        private readonly int LimitToExpand = 2; // A full heart is 2 pieces.

        public HeartContainer(LinkPlayer player, Vector2 pos) : base(player, pos)
        {
            sprite = ItemSpriteFactory.Instance.CreateHeartContainerSprite();
            this.pos = GetCenteredPos(pos, HitBox.Item.HeartContainer);
        }

        public override void Pickup()
        {
            SoundManager.Instance.Play(SoundEnum.Get_Heart);
            isPicked = true;
            player.IncreaseHeartLimit(LimitToExpand);
            player.Healing(LimitToExpand);
        }

        public override Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.Item.HeartContainer);
        }
    }
}
