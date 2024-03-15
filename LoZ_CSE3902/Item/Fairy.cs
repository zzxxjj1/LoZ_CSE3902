using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Fairy : IItem
    {
        private LinkPlayer player;
        private ISprite sprite;
        public Vector2 pos;
        public bool isPicked = false;
        private readonly int howManyHearts = 6;

        public Fairy(LinkPlayer player, Vector2 pos)
        {
            sprite = ItemSpriteFactory.Instance.CreateFairySprite();
            this.pos = pos;
            this.player = player;
        }

        public void Update()
        {
            sprite.Update();

            Random rdm = new Random();
            if (pos.X >= 230)
            {
                pos.X -= rdm.Next(0,3);
            }
            else if (pos.X <= 30)
            {
                pos.X += rdm.Next(0,5);
            }
            else
            {
                pos.X += rdm.Next(-1, 1);
            }

            if (pos.Y >= 150)
            {
                pos.Y -= rdm.Next(0,3);
            }
            else if (pos.Y <= 50)
            {
                pos.Y += rdm.Next(0,5);
            }
            else
            {
                pos.Y += rdm.Next(-1, 1);
            }

        }

        public void Pickup()
        {
            isPicked = true;
            SoundManager.Instance.Play(SoundEnum.Get_Item);
            player.Healing(howManyHearts);
        }
        public bool IsPicked()
        {
            return isPicked;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, pos);
        }

        public Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.Item.Fairy);
        }
    }
}