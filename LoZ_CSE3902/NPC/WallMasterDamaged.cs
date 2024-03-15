using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class WallMasterDamaged : INPCState
    {
        private WallMaster wallMaster;
        private ISprite sprite;
        private int frameToNextCut;
        private Boolean goNextFrame;
        private float SlideSpeed = 5, distance = 30;

        public WallMasterDamaged(WallMaster wallMaster)
        {
            this.wallMaster = wallMaster;
            frameToNextCut = 3;
            switch (this.wallMaster.spriteDirection)
            {
                case Direction.UpRight:
                    sprite = NPCSpriteFactory.Instance.CreateWallMasterURSprite();
                    break;
                case Direction.UpLeft:
                    sprite = NPCSpriteFactory.Instance.CreateWallMasterULSprite();
                    break;
                case Direction.DownLeft:
                    sprite = NPCSpriteFactory.Instance.CreateWallMasterDRSprite();
                    break;
                case Direction.DownRight:
                    sprite = NPCSpriteFactory.Instance.CreateWallMasterDLSprite();
                    break;
            }
            SoundManager.Instance.Play(SoundEnum.Enemy_Hit);
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Vector2 pos = wallMaster.pos;
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
                switch (wallMaster.currentDirection)
                {
                    case (Direction.Down):
                        wallMaster.pos.Y -= SlideSpeed;
                        if (isStop) wallMaster.currentState = new WallMasterWalkDown(wallMaster);
                        break;
                    case (Direction.Up):
                        wallMaster.pos.Y += SlideSpeed;
                        if (isStop) wallMaster.currentState = new WallMasterWalkUp(wallMaster);
                        break;
                    case (Direction.Left):
                        wallMaster.pos.X += SlideSpeed;
                        if (isStop) wallMaster.currentState = new WallMasterWalkLeft(wallMaster);
                        break;
                    case (Direction.Right):
                        wallMaster.pos.X -= SlideSpeed;
                        if (isStop) wallMaster.currentState = new WallMasterWalkRight(wallMaster);
                        break;
                    default: break;
                }
                frameToNextCut = 3;
            }
        }

        public void TakeDamage()
        {
        }

        public void ChangeDirection(float length)
        {
        }
    }
}
