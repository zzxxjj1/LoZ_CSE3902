using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Death : INPCState
    {
        private INPC npc;
        private ISprite sprite;
        private Vector2 pos;
        private Boolean goNextFrame, sound;
        private readonly int FramePerCut = 15, TotalCut = 4;
        private int frameToNextCut, animationLoopCounter = 1;

        public Death(INPC npc, Vector2 pos)
        {
            this.npc = npc;
            this.pos = pos;
            frameToNextCut = FramePerCut;
            sprite = NPCSpriteFactory.Instance.CreateExplosionSprite();
            sound = false;
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            sprite.Draw(spriteBatch, pos, goNextFrame);
            goNextFrame = false;
        }

        public void Update()
        {
            if (animationLoopCounter == TotalCut)
            {
                npc.IsAlive = false;
            }
            if (!sound)
            {
                sound = true;
                SoundManager.Instance.Play(SoundEnum.Enemy_Die);
            }
            frameToNextCut--;
            goNextFrame = frameToNextCut < 0;
            if (goNextFrame)
            {
                frameToNextCut = FramePerCut;
                animationLoopCounter++;
            }
        }

        public void TakeDamage()
        {
        }
    }
}
