using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Stalfos : INPC
    {
        public INPCState currentState;
        public Vector2 pos;
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        public bool IsAlive { get; set; } = true;
        int randomNum;
        public Game1 myGame;
        public System.Random random;
        public int framePerStep = 7;
        public int health;
        public Direction previousDirection, currentDirection;

        public Stalfos(Vector2 pos, Game1 game)
        {
            this.pos = pos;
            this.myGame = game;
            this.health = 2;
            random = new Random();
            this.RandomDirection();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentState.Draw(spriteBatch, pos.X, pos.Y);
        }

        public void Update()
        {
            currentState.Update();
        }

        public void RandomDirection()
        {
            randomNum = random.Next(1, 4);

            switch (randomNum)
            {
                case 1:
                    currentState = new StalfosWalkUp(this);
                    break;
                case 2:
                    currentState = new StalfosWalkDown(this);
                    break;
                case 3:
                    currentState = new StalfosWalkLeft(this);
                    break;
                case 4:
                    currentState = new StalfosWalkRight(this);
                    break;
            }
        }

        public void TakeDamage(Direction side)
        {
            previousDirection = currentDirection;
            currentDirection = side;
            currentState.TakeDamage();
        }

        public Rectangle GetRectangle()
        {
            if (currentState is Death) return HitBox.Empty;
            return HitBox.Create(pos.ToPoint(), HitBox.NPC.Stalfos);
        }

        public Vector2 GetPos()
        {
            return pos;
        }

        public void ChangeDirection(float length, Direction side)
        {
            switch (side)
            {
                case Direction.Down:
                    pos.Y -= length;
                    if (previousDirection is Direction.Up)
                    {
                        currentState = new StalfosWalkLeft(this);
                    }
                    else
                    {
                        currentState = new StalfosWalkUp(this);
                    }
                    break;
                case Direction.Up:
                    pos.Y += length;
                    if (previousDirection is Direction.Down)
                    {
                        currentState = new StalfosWalkRight(this);
                    }
                    else
                    {
                        currentState = new StalfosWalkDown(this);
                    }
                    break;
                case Direction.Left:
                    pos.X += length;
                    if (previousDirection is Direction.Right)
                    {
                        currentState = new StalfosWalkUp(this);
                    }
                    else
                    {
                        currentState = new StalfosWalkRight(this);
                    }
                    break;
                case Direction.Right:
                    pos.X -= length;
                    if (previousDirection is Direction.Left)
                    {
                        currentState = new StalfosWalkDown(this);
                    }
                    else
                    {
                        currentState = new StalfosWalkLeft(this);
                    }
                    break;
            }
        }
    }
}
