using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LoZ_CSE3902
{
    public class LinkDamagedSprite : ISprite
    {
        protected Texture2D texture;
        protected Rectangle frameRectangle;
        protected int totalFrames, frame_number;
        private Boolean colorTinted = true;

        public LinkDamagedSprite(Texture2D spriteSheet, Rectangle stepFrameSize)
        {
            texture = spriteSheet;
            frameRectangle = stepFrameSize;
            totalFrames = 5;
            frame_number = 0;

        }
        public void Update()
        {
            frame_number++;
            colorTinted = frame_number > totalFrames;
            if (colorTinted) frame_number = 0;
        }


        public void Draw(Vector2 destinationVector)
        {
            if (colorTinted)
            {
                GameUtility.Instance.SpriteBatch.Draw(texture, destinationVector, frameRectangle, Color.White);
                colorTinted = false;
            }
            else
            {
                GameUtility.Instance.SpriteBatch.Draw(texture, destinationVector, frameRectangle, Color.Orange);
            }
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

