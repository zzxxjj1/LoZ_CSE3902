using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LoZ_CSE3902
{
    public class Triforce : IItem
    {
        private LinkPlayer player;
        private ISprite sprite;
        private int maxCutFrame = 5, frameToNextCut;
        private Boolean goNextFrame = false;
        bool isPicked = false;

        public Vector2 pos;
        private Vector2 offset = new Vector2(8, 8);

        public Triforce(LinkPlayer player, Vector2 pos)
        {
            this.player = player;
            this.pos = GetCenteredPos(pos, HitBox.Item.Triforce);
            this.pos += offset;
            sprite = ItemSpriteFactory.Instance.CreateTriforceSprite();
            frameToNextCut = maxCutFrame;
        }

        public void Update()
        {
            // A piece of Triforce stand here
            // blink by Boolean goNextFrame
            // see Draw()
            
        }
        protected static Vector2 GetCenteredPos(Vector2 pos, Point Size)
        {
            pos.X += (GameAttributes.Window.TileWidth - Size.X) / 2;
            pos.Y += (GameAttributes.Window.TileHeight - Size.Y) / 2;
            return pos;
        }
        public void Pickup()
        {
            // player.trifore++ ???;
            isPicked = true;
            player.game.gameState = new EndOfGameState(player.game);
            player.game.gameState.CommandSetUp();
        }
        public bool IsPicked()
        {
            return isPicked;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            goNextFrame = frameToNextCut < 0;
            sprite.Draw(spriteBatch, pos, goNextFrame);
            if (goNextFrame) frameToNextCut = maxCutFrame;
            frameToNextCut--;
        }

        public Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.Item.Triforce);
        }
    }
}