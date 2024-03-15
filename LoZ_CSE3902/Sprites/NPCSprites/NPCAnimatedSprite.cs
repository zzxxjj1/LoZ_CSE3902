using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LoZ_CSE3902
{
    public class NPCAnimatedSprite : ISprite
    {
        protected Texture2D texture;
        protected Rectangle frameRectangle;
        protected Rectangle[] frame;
        private int totalFrames, frame_number;

        public NPCAnimatedSprite(Texture2D spriteSheet, Rectangle stepFrameSize, int frameCount)
        {
            texture = spriteSheet;
            frameRectangle = stepFrameSize;
            totalFrames = frameCount - 1;
            frame_number = 0;

            frame = new Rectangle[frameCount];
            for (int i = 0; i <= totalFrames; i++)
            {
                int x = i * frameRectangle.Width;
                frame[i] = new Rectangle(x, 0, frameRectangle.Width, frameRectangle.Height);
            }
        }
        public void Update()
        {
            // update by state

            /*
            currentFrame++;
            if (currentFrame > totalFrames)    currentFrame = 0;
            */
        }
        public void Draw(SpriteBatch spritebatch, Vector2 destinationVector)
        {
            Draw(destinationVector);
        }
        public void Draw(SpriteBatch spritebatch, Vector2 destinationVector, bool goNextFrame)
        {
            Draw(destinationVector, goNextFrame);
        }

        public void Draw(Vector2 destinationVector)
        {
            GameUtility.Instance.SpriteBatch.Draw(texture, destinationVector, frameRectangle, Color.White);
        }
        public void Draw(Vector2 destinationVector, bool goNextFrame)
        {
            if (goNextFrame) frame_number++;
            if (frame_number > totalFrames) frame_number = 0;
            GameUtility.Instance.SpriteBatch.Draw(texture, destinationVector, frame[frame_number], Color.White);
        }
    }
}

