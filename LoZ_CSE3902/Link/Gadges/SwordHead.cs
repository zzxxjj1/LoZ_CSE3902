using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoZ_CSE3902
{
    public class SwordHead : IGadgetEffect
    {
        // our implementation doesn't support multi-hitbox for one object
        // need this extra class assigned for the sword.
        public Vector2 pos;
        public Direction side;

        public LinkPlayer Player
        {
            get { return null; }
        }

        public SwordHead(Vector2 pos, Direction side)
        {
            this.pos = pos;
            this.side = side;
        }

        public void Update()
        {

        }


        public void Draw(SpriteBatch sb)
        {
            GameUtility.Instance.DrawBoarder(GetRectangle(), Color.Red, 1);
        }

        public Rectangle GetRectangle()
        {
            Point position = GetPositionBySide(side);

            switch (side)
            {
                case Direction.Up:
                    return HitBox.Create(position, HitBox.Link.SwordUp);
                case Direction.Left:
                    return HitBox.Create(position, HitBox.Link.SwordSide);
                case Direction.Right:
                    return HitBox.Create(position, HitBox.Link.SwordSide);
                case Direction.Down:
                    return HitBox.Create(position, HitBox.Link.SwordUp);
                default:
                    throw new InvalidOperationException("Direction not supported.");
            }
        }

        private Point GetPositionBySide(Direction side)
        {
            Point offset;
            switch (side)
            {
                case Direction.Up:
                case Direction.Down:
                    offset = new Point(
                        (HitBox.Tile.X - HitBox.Link.SwordUp.X) / 2,
                        (HitBox.Tile.Y - HitBox.Link.SwordUp.Y) / 2);
                    break;
                case Direction.Left:
                case Direction.Right:
                    offset = new Point(
                        (HitBox.Tile.X - HitBox.Link.SwordSide.X) / 2,
                        (HitBox.Tile.Y - HitBox.Link.SwordSide.Y) / 2);
                    break;
                default:
                    throw new InvalidOperationException("Direction not supported.");
            }

            Point position = new Point(pos.ToPoint().X + offset.X, pos.ToPoint().Y + offset.Y);
            switch (side)
            {
                case Direction.Up:
                    position.Y -= HitBox.Link.Body.Y; break;
                case Direction.Down:
                    position.Y += HitBox.Link.Body.Y; break;
                case Direction.Left:
                    position.X -= HitBox.Link.Body.X; break;
                case Direction.Right:
                    position.X += HitBox.Link.Body.X; break;
                default:
                    throw new InvalidOperationException("Direction not supported.");
            }


            return position;

        }
    }
}
