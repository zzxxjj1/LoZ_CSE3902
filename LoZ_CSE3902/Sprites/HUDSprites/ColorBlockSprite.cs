using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoZ_CSE3902
{
    public class ColorBlockSprite : ISprite
    {
        private Rectangle source;
        private Texture2D texture;
        private Color color;
        public ColorBlockSprite(Texture2D spriteSheet, Point size, Color color)
        {
            source = new Rectangle(new Point(0), size);
            texture = spriteSheet;
            this.color = color;
        }

        public void Update() {}

        public void Draw(Vector2 destinationVector)
        {
            GameUtility.Instance.SpriteBatchHUD.Draw(texture, destinationVector, source, color);
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
