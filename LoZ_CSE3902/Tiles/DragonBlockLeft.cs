﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    class DragonBlockLeft : ITile
    {
        public Vector2 pos;
        private ISprite sprite;
        private bool isObstacle = true;
        public DragonBlockLeft(Vector2 pos)
        {
            this.pos = pos;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite = TilesSpriteFactory.Instance.CreateDragonBlockLeft();
            sprite.Draw(spriteBatch, pos);
        }
        public bool IsObstacle()
        {
            return isObstacle;
        }
        public Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.Tile);
        }
        public void Transit(Vector2 offset)
        {
            pos += offset;
        }
    }
}
