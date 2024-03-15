using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class GoriyaDamaged : INPCState
    {
        private Goriya goriya;
        private ISprite sprite;
        private int frameToNextCut;
        private Boolean goNextFrame, sound;
        private float SlideSpeed = 5, distance = 30;

        public GoriyaDamaged(Goriya goriya)
        {
            this.goriya = goriya;
            frameToNextCut = 3;
            switch (goriya.currentDirection)
            {
                case (Direction.Down):
                    sprite = NPCSpriteFactory.Instance.CreateGoriyaFrontDamagedSprite();
                    break;
                case (Direction.Up):
                    sprite = NPCSpriteFactory.Instance.CreateGoriyaBackDamagedSprite();
                    break;
                case (Direction.Left):
                    sprite = NPCSpriteFactory.Instance.CreateGoriyaLeftDamagedSprite();
                    break;
                case (Direction.Right):
                    sprite = NPCSpriteFactory.Instance.CreateGoriyaRightDamagedSprite();
                    break;
                default: break;
            }
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Vector2 pos = goriya.pos;
            sprite.Draw(spriteBatch, pos);
        }

        public void Update()
        {
            if (!sound)
            {
                sound = true;
                SoundManager.Instance.Play(SoundEnum.Enemy_Hit);
            }
            frameToNextCut--;
            goNextFrame = frameToNextCut < 0;
            if (goNextFrame)
            {
                distance -= SlideSpeed;
                Boolean isStop = distance <= 0; // will handle collision here.

                switch (goriya.currentDirection)
                {
                    case (Direction.Down):
                        goriya.pos.Y -= SlideSpeed;
                        if (isStop) goriya.currentState = new GoriyaWalkDown(goriya);
                        break;
                    case (Direction.Up):
                        goriya.pos.Y += SlideSpeed;
                        if (isStop) goriya.currentState = new GoriyaWalkUp(goriya);
                        break;
                    case (Direction.Left):
                        goriya.pos.X += SlideSpeed;
                        if (isStop) goriya.currentState = new GoriyaWalkLeft(goriya);
                        break;
                    case (Direction.Right):
                        goriya.pos.X -= SlideSpeed;
                        if (isStop) goriya.currentState = new GoriyaWalkRight(goriya);
                        break;
                    default: break;
                }

                frameToNextCut = 3;
            }
        }

        public void TakeDamage()
        {
        }
    }
}
