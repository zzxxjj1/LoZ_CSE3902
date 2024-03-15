using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class BossAttack : INPCState
    {
        private Boss boss;
        private ISprite sprite;
        private Fireball fireball1, fireball2, fireball3;
        private int frameToNextCut;
        private Boolean goNextFrame, sound;

        public BossAttack(Boss boss, Fireball fireball1, Fireball fireball2, Fireball fireball3)
        {
            this.boss = boss;
            this.fireball1 = fireball1;
            this.fireball2 = fireball2;
            this.fireball3 = fireball3;
            sprite = NPCSpriteFactory.Instance.CreateBossSprite();
            frameToNextCut = boss.framePerStep;
            sound = false;

            fireball1.currentState = new FireballMove(fireball1, boss.pos.X - 15, boss.pos.Y, true, 1, boss.myGame); 
            fireball2.currentState = new FireballMove(fireball2, boss.pos.X - 15, boss.pos.Y + 20, true, 2, boss.myGame);
            fireball3.currentState = new FireballMove(fireball3, boss.pos.X - 15, boss.pos.Y + 40, true, 3, boss.myGame);
        }


        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Vector2 pos = new Vector2(boss.pos.X, boss.pos.Y);
            sprite.Draw(spriteBatch, pos, goNextFrame);
            goNextFrame = false;
            fireball1.Draw(spriteBatch);
            fireball2.Draw(spriteBatch);
            fireball3.Draw(spriteBatch);

        }

        public void Update()
        {
            if (!sound)
            {
                sound = true;
                SoundManager.Instance.Play(SoundEnum.Boss_Scream1);
            }
            if (fireball2.pos.X < 2 * GameAttributes.Window.TileWidth)
            {
                fireball1.IsAlive = false;
                fireball2.IsAlive = false;
                fireball3.IsAlive = false;
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
            if (goNextFrame) { frameToNextCut = boss.framePerStep; }
            fireball1.Update();
            fireball2.Update();
            fireball3.Update();
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
