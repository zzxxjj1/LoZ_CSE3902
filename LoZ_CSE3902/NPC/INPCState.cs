using System;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public interface INPCState
    {
        void Draw(SpriteBatch spriteBatch, float xPos, float yPos);
        void Update();
        void TakeDamage();
    }
}
