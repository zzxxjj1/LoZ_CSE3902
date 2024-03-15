using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class PlayerResetPos : ICommand
    {
        private LinkPlayer link;
        private Direction side;
        Vector2 pos, previousPos;

        public PlayerResetPos(ICollider link, ICollider npc, Direction side)
        {
            this.link = (LinkPlayer)link;
            this.side = side;
        }

        public void Execute()
        {
            pos = link.GetPos();
            previousPos = link.GetPreviousPos();

            switch (side)
            {
                case Direction.Up:
                    pos.X = previousPos.X;
                    pos.Y = previousPos.Y;
                    break;

                case Direction.Down:
                    pos.X = previousPos.X;
                    pos.Y = previousPos.Y;
                    break;

                case Direction.Left:
                    pos.X = previousPos.X;
                    pos.Y = previousPos.Y;
                    break;

                case Direction.Right:
                    pos.X = previousPos.X;
                    pos.Y = previousPos.Y;
                    break;
            }
        }
    }
}
