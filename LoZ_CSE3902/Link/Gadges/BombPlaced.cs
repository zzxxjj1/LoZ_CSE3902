using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class BombPlaced : IGadgetEffect
    {
        public LinkPlayer Player
        {
            get { return player; }
        }

        private LinkPlayer player;
        private ISprite sprite, smokeSprite;
        public Vector2 pos, smokePos;

        private Boolean sound, explosionSound, goNextFrame;

        private const int BombPosOffset = 10, BombXOffset = 4;
        private const int IgniteFrameCount = 60;
        private const int ExplodeFrameCount = 4;
        private const int FramePerCut = 15;
        private int frameToNextCut;
        public int CurrentFrame, TotalFrames;
        

        public BombPlaced(LinkPlayer player)
        {
            this.player = player;
            sprite = ItemSpriteFactory.Instance.CreateBombSprite();
            smokeSprite = ItemSpriteFactory.Instance.CreateBombSmokeSprite();
            TotalFrames = IgniteFrameCount + ExplodeFrameCount;
            sound = false;
            explosionSound = false;

            pos = this.player.GetPos();
            switch (this.player.facingDirection)
            {
                case Direction.Down:
                    pos = new Vector2(pos.X + BombXOffset, 
                        pos.Y + BombPosOffset + LinkSpriteFactory.Instance.useItemFrontFrameSize.Height);
                    break;
                case Direction.Up:
                    pos = new Vector2(pos.X + BombXOffset,
                        pos.Y - BombPosOffset - ItemSpriteFactory.Instance.BombFrameSize.Height);
                    break;
                case Direction.Left:
                    pos = new Vector2(
                        pos.X - (BombPosOffset + ItemSpriteFactory.Instance.BombFrameSize.Width),
                        pos.Y);
                    break;
                case Direction.Right:
                    pos = new Vector2(
                        pos.X + (BombPosOffset + LinkSpriteFactory.Instance.useItemFrontFrameSize.Width),
                        pos.Y);
                    break;
                default: break;
            }
            smokePos = new Vector2(pos.X - 14, pos.Y - 15);
        }

        public void Update()
        {
            if (!sound)
            {
                sound = true;
                SoundManager.Instance.Play(SoundEnum.Bomb_Drop);
            }
            if (CurrentFrame < IgniteFrameCount)
            {
                CurrentFrame++;
            }
            else
            {
                if (!explosionSound)
                {
                    explosionSound = true;
                    SoundManager.Instance.Play(SoundEnum.Bomb_Blow);
                }
                frameToNextCut--;
                goNextFrame = frameToNextCut < 0;
                if (goNextFrame)
                {
                    frameToNextCut = FramePerCut;
                    CurrentFrame++;
                }
            }
            
            if (CurrentFrame >= TotalFrames) player.inventory.TerminateGadget();

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentFrame < IgniteFrameCount)
            {
                sprite.Draw(spriteBatch, pos);
            }
            if (CurrentFrame >= IgniteFrameCount && CurrentFrame <= TotalFrames)
            {
                //sprite.Draw(spriteBatch, pos);
                smokeSprite.Draw(spriteBatch, smokePos, goNextFrame);
                goNextFrame = false;
                if (HitBox.showBoarder)
                    GameUtility.Instance.DrawBoarder(GetRectangle(), Color.Red, 1);
            }

        }

        public Rectangle GetRectangle()
        {
            if (CurrentFrame < IgniteFrameCount) return HitBox.Empty;
            return HitBox.Create(pos.ToPoint(), HitBox.Link.BombExplode, new Point(16, 8));
        }
    }
}