using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace LoZ_CSE3902
{
    public class NumberSprite : ISprite
    {
        public Texture2D texture;
        public Rectangle TextureSize;
        public Rectangle[] DigitRectangles;
        public Point charSize;
        public List<char> charList;
        public List<Rectangle> sources;
        private int previousCount = -1;

        public NumberSprite(List<char> charList, Texture2D spriteSheet, Rectangle TextureSize, Point charSize, Point ColRow, Point Offset)
        {
            texture = spriteSheet;
            this.TextureSize = TextureSize;
            this.charList = charList;
            this.charSize = charSize;
            sources = new List<Rectangle>();

            DigitRectangles = new Rectangle[ColRow.X * ColRow.Y];
            for (int Row = 0; Row < ColRow.Y; Row++)
            {
                for (int Col = 0; Col < ColRow.X; Col++)
                {
                    int x = TextureSize.X + Col * (charSize.X + Offset.X);
                    int y = TextureSize.Y + Row * (charSize.Y + Offset.Y);
                    DigitRectangles[Row * ColRow.Y + Col] = 
                        new Rectangle(x, y, charSize.X, charSize.Y);
                }
            }
            SetCount(0);
        }

        public void Update() { }
        public void SetCount(int count) 
        {
            if (count == previousCount) return;
            previousCount = count;

            int[] index = new int[3];
            sources = new List<Rectangle>();

            switch (count)
            {
                case int n when (n < 10):
                    index[0] = charList.FindIndex(a => a == 'x');
                    index[1] = charList.FindIndex(a => a == '0');
                    index[2] = charList.FindIndex(a => a == count + '0');
                    break;
                case int n when (n < 100):
                    index[0] = charList.FindIndex(a => a == 'x');
                    index[1] = charList.FindIndex(a => a == count / 10 + '0');
                    count %= 10;
                    index[2] = charList.FindIndex(a => a == count + '0');
                    break;
                case int n when (n < 1000):
                    index[0] = charList.FindIndex(a => a == count / 100 + '0');
                    count %= 100;
                    index[1] = charList.FindIndex(a => a == count / 10 + '0');
                    count %= 10;
                    index[2] = charList.FindIndex(a => a == count + '0');
                    break;
                default: break;
            }
            
            foreach (int i in index)
            {
                sources.Add(DigitRectangles[i]);
            }
        }

        public void Draw(Vector2 destPos)
        {
            foreach (var source in sources)
            {
                GameUtility.Instance.SpriteBatchHUD.Draw(texture, destPos, source, Color.White);
                destPos.X += charSize.X;
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
