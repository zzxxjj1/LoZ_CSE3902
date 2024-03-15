using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class GelWalkRight : INPCState
    {
        private Gel gel;
        private ISprite sprite;
        private int counter, frameToNextCut;
        private Boolean goNextFrame;

        public GelWalkRight(Gel gel)
        {
            this.gel = gel;
            sprite = NPCSpriteFactory.Instance.CreateGelSprite();
            frameToNextCut = gel.framePerStep;
            this.gel.previousDirection = this.gel.currentDirection;
            this.gel.currentDirection = Direction.Right;
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Vector2 pos = new Vector2(gel.pos.X, gel.pos.Y);
            sprite.Draw(spriteBatch, pos, goNextFrame);
            goNextFrame = false;
        }

        public void Update()
        {
            if (counter == 5)
            {
                gel.RandomDirection();
            }

            frameToNextCut--;
            goNextFrame = frameToNextCut < 0;
            if (goNextFrame)
            {
                gel.pos.X += 3;
                counter++;
                frameToNextCut = gel.framePerStep;
            }
        }

        public void TakeDamage()
        {
            gel.currentState = new Death(gel, gel.pos);
        }
    }
}
