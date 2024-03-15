using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public interface IGadgetEffect : ICollider
    {
        public LinkPlayer Player { get;}
        public void Update();
        public void Draw(SpriteBatch spriteBatch);
    }
}
