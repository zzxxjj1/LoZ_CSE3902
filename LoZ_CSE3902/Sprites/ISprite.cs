using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public interface ISprite
    {
        void Update();
        // for idle sprites
        void Draw(SpriteBatch spritebatch, Vector2 destinationVector);
        //for animated sprites
        void Draw(SpriteBatch spritebatch, Vector2 destinationVector, bool goNextFrame);

        // overloads when using GameUtility's spritebatch
        void Draw(Vector2 destinationVector);
        void Draw(Vector2 destinationVector, bool goNextFrame);
    }
}
