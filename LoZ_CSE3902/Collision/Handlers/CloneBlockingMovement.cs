using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class CloneBlockingMovement : ICommand
    {
        private LinkClone player;
        private Direction side;
        private float penetration_length;

        public CloneBlockingMovement(LinkClone player, Direction side, float length)
        {
            this.player = player;
            this.side = side;
            this.penetration_length = length;
        }

        public CloneBlockingMovement(ICollider player, ICollider tile, Direction side)
        {
            this.player = (LinkClone)player;
            this.side = side;
            penetration_length = CollisionHandler.GetPenetrationLength(player, tile, side);
        }

        public void Execute()
        {
           Vector2 pos = player.GetPos();
            switch (side)
            {
                case Direction.Up:
                    pos.Y = pos.Y+penetration_length;
                    break;
                case Direction.Down:
                    pos.Y = pos.Y-penetration_length;
                    break;
                case Direction.Left:
                    pos.X = pos.X + penetration_length;
                    break;
                case Direction.Right:
                    pos.X = pos.X- penetration_length;
                    break;
            }
            player.SetPos(pos);
        }
    }
}
