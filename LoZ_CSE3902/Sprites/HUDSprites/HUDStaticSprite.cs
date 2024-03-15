using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoZ_CSE3902
{
    public class HUDStaticSprite : ISprite
    {
        public Texture2D texture;
        public Rectangle frameRectangle;

        public HUDStaticSprite(Texture2D spriteSheet, Rectangle frameSize)
        {
            texture = spriteSheet;
            frameRectangle = frameSize;
        }

        public void Update() {}

        public void Draw(Vector2 destinationVector)
        {
            GameUtility.Instance.SpriteBatchHUD.Draw(texture, destinationVector, frameRectangle, Color.White);
        }
        public void Draw(Vector2 destinationVector, bool goNextFrame)
        {
            Draw(destinationVector);
        }
        public void Draw(SpriteBatch spritebatch, Vector2 destinationVector)
        {
            Draw(destinationVector);
        }
        public void Draw(SpriteBatch spritebatch, Vector2 destinationVector, bool goNextFrame)
        {
            Draw(destinationVector, goNextFrame);
        }
    }
}
