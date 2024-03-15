using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace LoZ_CSE3902
{
    public abstract class StaticItem : IItem
    {
        protected LinkPlayer player;
        protected ISprite sprite;
        public Vector2 pos;
        public bool isPicked = false;

        public StaticItem(LinkPlayer player, Vector2 pos)
        {
            this.pos = pos;
            this.player = player;
            // need to assign sprite in derived class
        }

        public void Update()
        {
            // non-animating, non-moving
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, pos);
        }

        protected static Vector2 GetCenteredPos(Vector2 pos, Point Size)
        {
            pos.X += (GameAttributes.Window.TileWidth - Size.X) / 2;
            pos.Y += (GameAttributes.Window.TileHeight - Size.Y) / 2;
            return pos;
        }

        // set isPicked to true, then modify player.inventory
        public abstract void Pickup(); 

        public bool IsPicked()
        {
            return isPicked;
        }
        
        public abstract Rectangle GetRectangle();
    }
}
