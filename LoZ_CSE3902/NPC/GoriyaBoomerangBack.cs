using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class GoriyaBoomerangBack : INPCState
    {
        private GoriyaBoomerang goriyaBoomerang;
        private ISprite sprite;
        private int frameToNextCut;
        private Boolean goNextFrame;

        public GoriyaBoomerangBack(GoriyaBoomerang GoriyaBoomerang, float xPos, float yPos, Direction direction, Game1 game)
        {
            this.goriyaBoomerang = GoriyaBoomerang;
            this.goriyaBoomerang.pos.X = xPos;
            this.goriyaBoomerang.pos.Y = yPos;
            this.goriyaBoomerang.direction = direction;
            this.goriyaBoomerang.IsAlive = true;
            this.goriyaBoomerang.myGame = game;
            sprite = NPCSpriteFactory.Instance.CreateGoriyaBoomerangSprite();
            frameToNextCut = GoriyaBoomerang.framePerStep;
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Vector2 pos = new Vector2(goriyaBoomerang.pos.X, goriyaBoomerang.pos.Y);
            sprite.Draw(spriteBatch, pos, goNextFrame);
            goNextFrame = false;
        }

        public void Update()
        {
            switch (goriyaBoomerang.direction)
            {
                case Direction.Down:
                    if(goriyaBoomerang.pos.Y <= goriyaBoomerang.initialPos.Y)
                    {
                        goriyaBoomerang.IsAlive = false;
                    }
                    else
                    {
                        goriyaBoomerang.pos.Y -= 2;
                    }
                    break;
                case Direction.Up:
                    if (goriyaBoomerang.pos.Y >= goriyaBoomerang.initialPos.Y)
                    {
                        goriyaBoomerang.IsAlive = false;
                    }
                    else
                    {
                        goriyaBoomerang.pos.Y += 2;
                    }
                    break;
                case Direction.Left:
                    if (goriyaBoomerang.pos.X >= goriyaBoomerang.initialPos.X)
                    {
                        goriyaBoomerang.IsAlive = false;
                    }
                    else
                    {
                        goriyaBoomerang.pos.X += 2;
                    }
                    break;
                case Direction.Right:
                    if (goriyaBoomerang.pos.X <= goriyaBoomerang.initialPos.X)
                    {
                        goriyaBoomerang.IsAlive = false;
                    }
                    else
                    {
                        goriyaBoomerang.pos.X -= 2;
                    }
                    break;
            }
            frameToNextCut--;
            goNextFrame = frameToNextCut < 0;
            if (goNextFrame) { frameToNextCut = goriyaBoomerang.framePerStep; }

        }

        public void TakeDamage()
        {
        }
    }
}
