using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class BossDamaged : INPCState
    {
        private Boss boss;
        private ISprite sprite;
        private Fireball fireball1, fireball2, fireball3;
        private int frameToNextCut, counter;
        private Boolean goNextFrame;

        public BossDamaged(Boss boss, Fireball fireball1, Fireball fireball2, Fireball fireball3)
        {
            this.boss = boss;
            this.fireball1 = fireball1;
            this.fireball2 = fireball2;
            this.fireball3 = fireball3;
            sprite = NPCSpriteFactory.Instance.CreateBossDamagedSprite();
            frameToNextCut = 7;
            SoundManager.Instance.Play(SoundEnum.Enemy_Hit);
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Vector2 pos = new Vector2(boss.pos.X, boss.pos.Y);
            sprite.Draw(spriteBatch, pos, goNextFrame);
            goNextFrame = false;
        }

        public void Update()
        {
            if (counter == 5)
            {
                if (boss.left)
                {
                    boss.currentState = new BossWalkRight(boss, fireball1, fireball2, fireball3);
                }
                else
                {
                    boss.currentState = new BossWalkLeft(boss, fireball1, fireball2, fireball3);
                }
            }
            frameToNextCut--;
            goNextFrame = frameToNextCut < 0;
            if (goNextFrame) {
                counter++;
                frameToNextCut = 7;
            }
        }

        public void TakeDamage()
        {
        }
    }
}
