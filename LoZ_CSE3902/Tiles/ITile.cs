using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace LoZ_CSE3902
{
    public interface ITile : ICollider
    {
        bool IsObstacle();
        void Update();
        void Draw(SpriteBatch spriteBatch);
        void Transit(Vector2 offset);
    }
}
