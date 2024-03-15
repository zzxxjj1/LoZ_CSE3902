using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class GoriyaBoomerangOut : INPCState
    {
        private GoriyaBoomerang goriyaBoomerang;
        private ISprite sprite;
        private int frameToNextCut;
        private Boolean goNextFrame, inRoom;

        public GoriyaBoomerangOut(GoriyaBoomerang GoriyaBoomerang, float xPos, float yPos, Direction direction, Game1 game)
        {
            this.goriyaBoomerang = GoriyaBoomerang;
            this.goriyaBoomerang.pos.X = xPos;
            this.goriyaBoomerang.pos.Y = yPos;
            this.goriyaBoomerang.initialPos.X = xPos;
            this.goriyaBoomerang.initialPos.Y = yPos;
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
            if (!inRoom)
            {
                if (!(goriyaBoomerang.myGame.gameState is GamePlayState))
                    throw new InvalidOperationException(
                        "GoriyaBoomerangOut: require GamePlayState");
                GamePlayState gameplay = (GamePlayState)goriyaBoomerang.myGame.gameState;
                gameplay.room.AddEntity(goriyaBoomerang);
                inRoom = true;
            }
            switch (goriyaBoomerang.direction)
            {
                case Direction.Down:
                    goriyaBoomerang.pos.Y += 2;
                    break;
                case Direction.Up:
                    goriyaBoomerang.pos.Y -= 2;
                    break;
                case Direction.Left:
                    goriyaBoomerang.pos.X -= 2;
                    break;
                case Direction.Right:
                    goriyaBoomerang.pos.X += 2;
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
