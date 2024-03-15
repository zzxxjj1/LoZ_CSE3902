using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class BossWalkLeft :INPCState
    {
        private Boss boss;
        private ISprite sprite;
        private Fireball fireball1, fireball2, fireball3;
        private int frameToNextCut, counter;
        private Boolean goNextFrame;

        public BossWalkLeft(Boss boss, Fireball fireball1, Fireball fireball2, Fireball fireball3)
        {
            this.boss = boss;
            this.boss.left = true;
            this.fireball1 = fireball1;
            this.fireball2 = fireball2;
            this.fireball3 = fireball3;
            sprite = NPCSpriteFactory.Instance.CreateBossSprite();
            frameToNextCut = boss.framePerStep;
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Vector2 pos = new Vector2(boss.pos.X, boss.pos.Y);
            sprite.Draw(spriteBatch, pos, goNextFrame);
            goNextFrame = false;
        }

        public void Update()
        {
            if (counter == 20)
            {
                boss.currentState = new BossAttack(boss, fireball1, fireball2, fireball3);
            }

            frameToNextCut--;
            goNextFrame = frameToNextCut < 0;
            if (goNextFrame) {
                boss.pos.X--;
                counter++;
                frameToNextCut = boss.framePerStep;
            }
        }

        public void TakeDamage()
        {
            boss.health--;

            if (boss.health == 0)
            {
                boss.currentState = new Death(boss, boss.pos);
            }
            else
            {
                boss.currentState = new BossDamaged(boss, fireball1, fireball2, fireball3);
            }
        }
    }
}
