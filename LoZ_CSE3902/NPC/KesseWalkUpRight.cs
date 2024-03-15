using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class KesseWalkUpRight : INPCState
    {
        private Kesse kesse;
        private ISprite sprite;
        private int counter, frameToNextCut;
        private Boolean goNextFrame;

        public KesseWalkUpRight(Kesse kesse)
        {
            this.kesse = kesse;
            sprite = NPCSpriteFactory.Instance.CreateKesseSprite();
            frameToNextCut = kesse.framePerStep;
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Vector2 pos = new Vector2(kesse.pos.X, kesse.pos.Y);
            sprite.Draw(spriteBatch, pos, goNextFrame);
            goNextFrame = false;
        }

        public void Update()
        {
            if (counter == 5)
            {
                kesse.RandomDirection();
            }

            frameToNextCut--;
            goNextFrame = frameToNextCut < 0;
            if (goNextFrame)
            {
                kesse.pos.Y -= 3;
                kesse.pos.X += 3;
                counter++;
                frameToNextCut = kesse.framePerStep;
            }
        }

        public void TakeDamage()
        {
            kesse.currentState = new Death(kesse, kesse.pos);
        }
    }
}
