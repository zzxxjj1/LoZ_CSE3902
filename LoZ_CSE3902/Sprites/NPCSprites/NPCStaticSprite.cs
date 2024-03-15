using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LoZ_CSE3902
{
    public class NPCStaticSprite : ISprite
    {
        protected Texture2D texture;
        protected Rectangle frameRectangle;

        public NPCStaticSprite(Texture2D spriteSheet, Rectangle stepFrameSize)
        {
            texture = spriteSheet;
            frameRectangle = stepFrameSize;
        }
        public void Update()
        {
            // non-animated
        }
        public void Draw(SpriteBatch spritebatch, Vector2 destinationVector)
        {
            Draw(destinationVector);
        }
        public void Draw(SpriteBatch spritebatch, Vector2 destinationVector, bool goNextFrame)
        {
            Draw(destinationVector);
        }

        public void Draw(Vector2 destinationVector)
        {
            GameUtility.Instance.SpriteBatch.Draw(texture, destinationVector, frameRectangle, Color.White);
        }
        public void Draw(Vector2 destinationVector, bool goNextFrame)
        {
            Draw(destinationVector);
        }
    }
}

