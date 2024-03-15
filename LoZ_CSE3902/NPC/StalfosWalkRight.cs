using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class StalfosWalkRight : INPCState
    {
        private Stalfos stalfos;
        private ISprite sprite;
        private int counter, frameToNextCut;
        private Boolean goNextFrame;

        public StalfosWalkRight(Stalfos stalfos)
        {
            this.stalfos = stalfos;
            this.stalfos.previousDirection = this.stalfos.currentDirection;
            this.stalfos.currentDirection = Direction.Right;
            sprite = NPCSpriteFactory.Instance.CreateStalfosSprite();
            frameToNextCut = stalfos.framePerStep;
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Vector2 pos = new Vector2(stalfos.pos.X, stalfos.pos.Y);
            sprite.Draw(spriteBatch, pos, goNextFrame);
            goNextFrame = false;
        }

        public void Update()
        {
            if (counter == 5)
            {
                stalfos.RandomDirection();
            }

            frameToNextCut--;
            goNextFrame = frameToNextCut < 0;
            if (goNextFrame)
            {
                stalfos.pos.X += 3;
                counter++;
                frameToNextCut = stalfos.framePerStep;
            }
        }


        public void TakeDamage()
        {
            stalfos.health--;

            if (stalfos.health == 0)
            {
                stalfos.currentState = new Death(stalfos, stalfos.pos);
            }
            else
            {
                stalfos.currentState = new StalfosDamaged(stalfos);
            }
        }
    }
}
