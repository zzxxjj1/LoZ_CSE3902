using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class GoriyaWalkDown : INPCState
    {
        private Goriya goriya;
        private ISprite sprite;
        private int counter, frameToNextCut;
        private Boolean goNextFrame;

        public GoriyaWalkDown(Goriya goriya)
        {
            this.goriya = goriya;
            this.goriya.count++;
            this.goriya.previousDirection = this.goriya.currentDirection;
            this.goriya.currentDirection = Direction.Down;
            sprite = NPCSpriteFactory.Instance.CreateGoriyaFrontSprite();
            frameToNextCut = goriya.framePerStep;
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Vector2 pos = new Vector2(goriya.pos.X, goriya.pos.Y);
            sprite.Draw(spriteBatch, pos, goNextFrame);
            goNextFrame = false;
        }

        public void Update()
        {
            if (counter == 5)
            {
                goriya.RandomDirection();
            }

            frameToNextCut--;
            goNextFrame = frameToNextCut < 0;
            if (goNextFrame)
            {
                goriya.pos.Y += 3;
                counter++;
                frameToNextCut = goriya.framePerStep;
            }
        }

        public void TakeDamage()
        {
            goriya.health--;

            if (goriya.health == 0)
            {
                goriya.currentState = new Death(goriya, goriya.pos);
            }
            else
            {
                goriya.currentState = new GoriyaDamaged(goriya);
            }
        }
    }
}
