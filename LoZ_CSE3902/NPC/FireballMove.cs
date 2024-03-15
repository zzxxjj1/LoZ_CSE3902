using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class FireballMove : INPCState
    {
        private Fireball fireball;
        private ISprite sprite;
        private int frameToNextCut;
        private Boolean goNextFrame, inRoom;

        public FireballMove(Fireball fireball, float xPos, float yPos, Boolean fire, int position, Game1 game)
        {
            this.fireball = fireball;
            this.fireball.pos.X = xPos;
            this.fireball.pos.Y = yPos;
            this.fireball.fire = fire;
            this.fireball.position = position;
            this.fireball.IsAlive = true;
            this.fireball.myGame = game;
            sprite = NPCSpriteFactory.Instance.CreateFireballSprite();
            frameToNextCut = fireball.framePerStep;
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            if (fireball.fire)
            {
                Vector2 pos = new Vector2(fireball.pos.X, fireball.pos.Y);
                sprite.Draw(spriteBatch, pos, goNextFrame);
                goNextFrame = false;
            }
        }

        public void Update()
        {
            if (!inRoom)
            {
                if (!(fireball.myGame.gameState is GamePlayState))
                    throw new InvalidOperationException(
                        "FireballMove: require GamePlayState");
                GamePlayState gameplay = (GamePlayState)fireball.myGame.gameState;
                gameplay.room.AddEntity(fireball);
                inRoom = true;
            }
            if (fireball.fire)
            {
                fireball.pos.X -= 2;
                if (fireball.position == 1) { fireball.pos.Y -= 1; }
                if (fireball.position == 3) { fireball.pos.Y += 1; }
                frameToNextCut--;
                goNextFrame = frameToNextCut < 0;
                if (goNextFrame) { frameToNextCut = fireball.framePerStep; }
            }
            
        }

        public void TakeDamage()
        {
        }

    }
}
