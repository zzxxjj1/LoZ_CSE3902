using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class GoriyaAttack : INPCState
    {
        private Goriya goriya;
        private ISprite sprite;
        private int frameToNextCut;
        private Boolean goNextFrame, sound;

        public GoriyaAttack(Goriya goriya)
        {
            this.goriya = goriya;
            frameToNextCut = goriya.framePerStep;
            switch (goriya.currentDirection)
            {
                case (Direction.Down):
                    sprite = NPCSpriteFactory.Instance.CreateGoriyaFrontSprite();
                    break;
                case (Direction.Up):
                    sprite = NPCSpriteFactory.Instance.CreateGoriyaBackSprite();
                    break;
                case (Direction.Left):
                    sprite = NPCSpriteFactory.Instance.CreateGoriyaLeftSprite();
                    break;
                case (Direction.Right):
                    sprite = NPCSpriteFactory.Instance.CreateGoriyaRightSprite();
                    break;
                default: break;
            }
            
            goriya.boomerang.currentState = new GoriyaBoomerangOut(goriya.boomerang, goriya.pos.X, goriya.pos.Y,
                goriya.currentDirection, goriya.myGame);
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Vector2 pos = goriya.pos;
            sprite.Draw(spriteBatch, pos);
            goriya.boomerang.Draw(spriteBatch);
        }

        public void Update()
        {
            if (!sound) {
                sound = true;
                SoundManager.Instance.Play(SoundEnum.Arrow_Boomerang);
            }

            if (goriya.boomerang.IsAlive == false)
            {
                switch (goriya.currentDirection)
                {
                    case Direction.Up:
                        goriya.currentState = new GoriyaWalkUp(goriya);
                        break;
                    case Direction.Down:
                        goriya.currentState = new GoriyaWalkDown(goriya);
                        break;
                    case Direction.Left:
                        goriya.currentState = new GoriyaWalkLeft(goriya);
                        break;
                    case Direction.Right:
                        goriya.currentState = new GoriyaWalkRight(goriya);
                        break;
                }
            }
            frameToNextCut--;
            goNextFrame = frameToNextCut < 0;
            if (goNextFrame) { frameToNextCut = goriya.framePerStep; }
            goriya.boomerang.Update();
        }

        public void TakeDamage()
        {
            goriya.health--;

            if (goriya.health == 0)
            {
                goriya.boomerang.IsAlive = false;
                goriya.currentState = new Death(goriya, goriya.pos);
            }
            else
            {
                goriya.currentState = new GoriyaDamaged(goriya);
            }
        }
    }
}
