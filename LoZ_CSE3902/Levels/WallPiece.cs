using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoZ_CSE3902
{
    public class WallPiece : ICollider
    {
        // our implementation doesn't support multi-hitbox for one object
        // need this extra class assigned for the dungeon wall.
        public Vector2 pos;
        public Direction side;
        
        public WallPiece(Vector2 pos, Direction side)
        {
            this.pos = pos;
            this.side = side;
        }

        public Rectangle GetRectangle()
        {
            if(side == Direction.Down || side == Direction.Up)
            {
                return HitBox.Create(pos.ToPoint(), HitBox.Room.WallPieceUp);
            }
            else
            {
                return HitBox.Create(pos.ToPoint(), HitBox.Room.WallPieceSide);
            }
            
        }
    }
}
