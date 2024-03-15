using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LoZ_CSE3902
{
    public class ItemSprite : ISprite
    {
        private Texture2D texture;
        private Rectangle frameRectangle;
        private Rectangle[] frame;
        private int totalFrames, frame_number;

        public ItemSprite(Texture2D spriteSheet, Rectangle stepFrameSize, int frameCount)
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
        }

        // draw non-animated 1st frame
        public void Draw(Vector2 destinationVector)
        {
            GameUtility.Instance.SpriteBatch.Draw(texture, destinationVector, frameRectangle, Color.White);
        }

        // draw frame by frame
        public void Draw(Vector2 destinationVector, bool goNextFrame)
        {
            if (goNextFrame) frame_number++;
            if (frame_number > totalFrames) frame_number = 0;
            GameUtility.Instance.SpriteBatch.Draw(texture, destinationVector, frame[frame_number], Color.White);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 destinationVector)
        {
            spritebatch.Draw(texture, destinationVector, frameRectangle, Color.White);
        }
        public void Draw(SpriteBatch spritebatch, Vector2 destinationVector, bool goNextFrame)
        {
            Draw(destinationVector, goNextFrame);
        }
    }
}