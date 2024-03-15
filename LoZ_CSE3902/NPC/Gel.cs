using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Gel : INPC
    {
        public INPCState currentState;
        public Vector2 pos;
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        int randomNum;
        public Game1 myGame;
        public System.Random random;
        public int framePerStep = 5;
        public bool IsAlive { get; set; }
        public Direction previousDirection, currentDirection;

        public Gel(Vector2 pos, Game1 game)
        {
            this.pos = pos;
            this.myGame = game;
            random = new Random();
            this.RandomDirection();
            IsAlive = true;
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
                    currentState = new GelWalkUp(this);
                    break;
                case 2:
                    currentState = new GelWalkDown(this);
                    break;
                case 3:
                    currentState = new GelWalkLeft(this);
                    break;
                case 4:
                    currentState = new GelWalkRight(this);
                    break;
            }
        }

        public void TakeDamage(Direction side)
        {
            currentState.TakeDamage();
        }

        public Rectangle GetRectangle()
        {
            if (currentState is Death) return HitBox.Empty;
            return HitBox.Create(pos.ToPoint(), HitBox.NPC.Gel);
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
                        currentState = new GelWalkLeft(this);
                    }
                    else
                    {
                        currentState = new GelWalkUp(this);
                    }
                    break;
                case Direction.Up:
                    pos.Y += length;
                    if (previousDirection is Direction.Down)
                    {
                        currentState = new GelWalkRight(this);
                    }
                    else
                    {
                        currentState = new GelWalkDown(this);
                    }
                    break;
                case Direction.Left:
                    pos.X += length;
                    if (previousDirection is Direction.Right)
                    {
                        currentState = new GelWalkUp(this);
                    }
                    else
                    {
                        currentState = new GelWalkRight(this);
                    }
                    break;
                case Direction.Right:
                    pos.X -= length;
                    if (previousDirection is Direction.Left)
                    {
                        currentState = new GelWalkDown(this);
                    }
                    else
                    {
                        currentState = new GelWalkLeft(this);
                    }
                    break;
            }
        }
    }
}
