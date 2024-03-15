using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class WallMasterWalkDown : INPCState
    {
        private WallMaster wallMaster;
        private ISprite sprite;
        private int counter, frameToNextCut;
        private Boolean goNextFrame;

        public WallMasterWalkDown(WallMaster wallMaster)
        {
            this.wallMaster = wallMaster;
            frameToNextCut = wallMaster.framePerStep;
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
            this.wallMaster.previousDirection = this.wallMaster.currentDirection;
            this.wallMaster.currentDirection = Direction.Down;
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Vector2 pos = new Vector2(wallMaster.pos.X, wallMaster.pos.Y);
            sprite.Draw(spriteBatch, pos, goNextFrame);
            goNextFrame = false;
        }

        public void Update()
        {
            if (counter == 5)
            {
                wallMaster.RandomDirection();
            }

            frameToNextCut--;
            goNextFrame = frameToNextCut < 0;
            if (goNextFrame)
            {
                wallMaster.pos.Y += 3;
                counter++;
                frameToNextCut = wallMaster.framePerStep;
            }
        }

        public void TakeDamage()
        {
            wallMaster.health--;

            if (wallMaster.health == 0)
            {
                wallMaster.currentState = new Death(wallMaster, wallMaster.pos);
            }
            else
            {
                wallMaster.currentState = new WallMasterDamaged(wallMaster);
            }
        }
    }
}
