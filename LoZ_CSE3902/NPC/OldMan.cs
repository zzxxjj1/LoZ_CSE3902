using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class OldMan : INPC
    {
        public Vector2 pos;
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        public bool IsAlive { get; set; } = true; // Old Man lives forever.

        private ISprite sprite;
        public OldMan (Vector2 pos, Game1 game)
        {
            this.pos = pos;
            sprite = NPCSpriteFactory.Instance.CreateOldManSprite();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, pos);
        }

        public Rectangle GetRectangle()
        {
            return HitBox.Empty;
            //return HitBox.Create(pos.ToPoint(), HitBox.NPC.OldMan);
        }

        public void TakeDamage(Direction side)
        {
        }

        public void ChangeDirection(float length, Direction side)
        {
        }

        public void Update()
        {
        }

        public Vector2 GetPos()
        {
            return pos;
        }
    }
}
