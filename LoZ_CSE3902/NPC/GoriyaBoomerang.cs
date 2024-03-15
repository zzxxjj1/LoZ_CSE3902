using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class GoriyaBoomerang : INPC
    {
        public INPCState currentState;
        public bool back;
        public Vector2 pos, initialPos;
        public int framePerStep = 5;
        public Direction direction;
        public Game1 myGame;
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        public bool IsAlive { get; set; }

        public GoriyaBoomerang(float xPos, float yPos, Direction direction, Game1 game)
        {
            this.pos.X = xPos;
            this.pos.Y = yPos;
            this.direction = direction;
            IsAlive = true;
            back = false;
            myGame = game;
            currentState = new GoriyaBoomerangOut(this, xPos, yPos, direction, myGame);
        }

        public void ChangeDirection(float length, Direction side)
        {
            switch (side)
            {
                case Direction.Down:
                    pos.Y -= length;
                    break;
                case Direction.Up:
                    pos.Y += length;
                    break;
                case Direction.Left:
                    pos.X += length;
                    break;
                case Direction.Right:
                    pos.X -= length;
                    break;
            }
            currentState = new GoriyaBoomerangBack(this, pos.X, pos.Y, direction, myGame);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentState.Draw(spriteBatch, pos.X, pos.Y);
        }

        public Vector2 GetPos()
        {
            return pos;
        }

        public Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.NPC.GoriyaBoomerang);
        }

        public void TakeDamage(Direction side)
        {
        }

        public void Update()
        {
            currentState.Update();
        }
    }
}
