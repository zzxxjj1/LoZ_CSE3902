using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoZ_CSE3902
{
    public class HeartSprite: ISprite
    {
        public Texture2D texture;
        public Rectangle HeartTextureSize;
        public Rectangle[] HeartRectangles; // empty, half, full
        public Point heartSize, displayColRow;
        public List<Rectangle> sources;
        private int previousCount, previousMax;

        private const int OneFullHeart = 2;
        private readonly Rectangle PlaceholderRectangle = new Rectangle(560, 67, 8, 8);

        public enum Heart
        {
            Empty,
            Half,
            Full
        }

        public HeartSprite(Texture2D spriteSheet, Rectangle heartTextureSize, Point heartSize, Point offSet)
        {
            texture = spriteSheet;
            this.HeartTextureSize = heartTextureSize;
            this.heartSize = heartSize;
            this.sources = new List<Rectangle>();
            displayColRow = new Point(8, 2);

            HeartRectangles = new Rectangle[3];
            for (int col = 0; col < 3; col++)
            {
                int x = HeartTextureSize.X + col * (heartSize.X + offSet.X);
                int y = HeartTextureSize.Y;
                HeartRectangles[col] = new Rectangle(x, y, heartSize.X, heartSize.Y);
            }

            previousCount = -1;
            previousMax = -1;
            SetHeart(0, 0);
        }
        public void Update() { }
        public void SetHeart(int heartCount, int maxHeart)
        {
            if (heartCount == previousCount && maxHeart == previousMax) return;
            previousCount = heartCount;
            previousMax = maxHeart;

            sources = new List<Rectangle>(displayColRow.X * displayColRow.Y);

            int fullHeart = heartCount / OneFullHeart;
            int halfHeart = heartCount % OneFullHeart;
            int emptyHeart = maxHeart / OneFullHeart - fullHeart - halfHeart;
            int placeholder = displayColRow.X * displayColRow.Y
                - fullHeart - halfHeart - emptyHeart;

            int index = 0;
            for (int i = 0; i < fullHeart; i++)
            {
                sources.Add(HeartRectangles[(int)Heart.Full]);
                index++;
            }
            for (int i = 0; i < halfHeart; i++)
            {
                sources.Add(HeartRectangles[(int)Heart.Half]);
                index++;
            }
            for (int i = 0; i < emptyHeart; i++)
            {
                sources.Add(HeartRectangles[(int)Heart.Empty]);
                index++;
            }
            for (int i = 0; i < placeholder; i++)
            {
                sources.Add(PlaceholderRectangle);
                index++;
            }
        }

        public void Draw(Vector2 destPos) {
            Vector2 destOrigin = destPos;
            int index = 0;
            // from left to right, down to up as the original game
            destPos.Y += heartSize.Y * (displayColRow.Y - 1); 
            for (int row = 0; row < displayColRow.Y; row++)
            {
                for (int col = 0; col < displayColRow.X; col++)
                {
                    GameUtility.Instance.SpriteBatchHUD.Draw(texture, destPos, sources[index], Color.White);
                    destPos.X += heartSize.X;
                    index++;
                }
                destPos.X = destOrigin.X;
                destPos.Y -= heartSize.Y;
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
