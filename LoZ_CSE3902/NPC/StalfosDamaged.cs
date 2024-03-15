using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class StalfosDamaged : INPCState
    {
        private Stalfos stalfos;
        private ISprite sprite;
        private int frameToNextCut;
        private Boolean goNextFrame;
        private float SlideSpeed = 5, distance = 30;

        public StalfosDamaged(Stalfos stalfos)
        {
            this.stalfos = stalfos;
            frameToNextCut = 3;
            sprite = NPCSpriteFactory.Instance.CreateStalfosDamagedSprite();
            SoundManager.Instance.Play(SoundEnum.Enemy_Hit);
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Vector2 pos = stalfos.pos;
            sprite.Draw(spriteBatch, pos);
        }

        public void Update()
        {
            frameToNextCut--;
            goNextFrame = frameToNextCut < 0;
            if (goNextFrame)
            {
                distance -= SlideSpeed;
                Boolean isStop = distance <= 0; // will handle collision here.

                switch (stalfos.currentDirection)
                {
                    case (Direction.Down):
                        stalfos.pos.Y -= SlideSpeed;
                        if (isStop) stalfos.currentState = new StalfosWalkDown(stalfos);
                        break;
                    case (Direction.Up):
                        stalfos.pos.Y += SlideSpeed;
                        if (isStop) stalfos.currentState = new StalfosWalkUp(stalfos);
                        break;
                    case (Direction.Left):
                        stalfos.pos.X += SlideSpeed;
                        if (isStop) stalfos.currentState = new StalfosWalkLeft(stalfos);
                        break;
                    case (Direction.Right):
                        stalfos.pos.X -= SlideSpeed;
                        if (isStop) stalfos.currentState = new StalfosWalkRight(stalfos);
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
