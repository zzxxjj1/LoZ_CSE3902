using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Goriya : INPC
    {
        public INPCState currentState;
        public Vector2 pos;
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        public bool IsAlive { get; set; }

        int randomNum;
        public Game1 myGame;
        public System.Random random;
        public int framePerStep = 10;
        public int health, count;
        public Direction previousDirection, currentDirection;
        public GoriyaBoomerang boomerang;

        public Goriya(Vector2 pos, Game1 game)
        {
            this.pos = pos;
            this.myGame = game;
            this.health = 3;
            IsAlive = true;
            random = new Random();
            this.RandomDirection();
            boomerang = new GoriyaBoomerang(pos.X, pos.Y, currentDirection, myGame);
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
            if(count > 3)
            {
                count = 0;
                currentState = new GoriyaAttack(this);
            }
            else
            {
                switch (randomNum)
                {
                    case 1:
                        currentState = new GoriyaWalkUp(this);
                        break;
                    case 2:
                        currentState = new GoriyaWalkDown(this);
                        break;
                    case 3:
                        currentState = new GoriyaWalkLeft(this);
                        break;
                    case 4:
                        currentState = new GoriyaWalkRight(this);
                        break;
                }
            }
        }

        public Rectangle GetRectangle()
        {
            if (currentState is Death) return HitBox.Empty;
            if (currentDirection == Direction.Down || currentDirection == Direction.Up)
            {
                return HitBox.Create(pos.ToPoint(), HitBox.NPC.GoriyaFront);
            }
            else
            {
                return HitBox.Create(pos.ToPoint(), HitBox.NPC.GoriyaSide);
            }
        }

        public Vector2 GetPos()
        {
            return pos;
        }

        public void TakeDamage(Direction side)
        {
            previousDirection = currentDirection;
            currentDirection = side;
            currentState.TakeDamage();
        }

        public void ChangeDirection(float length, Direction side)
        {
            switch (side)
            {
                case Direction.Down:
                    pos.Y -= length;
                    if (previousDirection is Direction.Up)
                    {
                        currentState = new GoriyaWalkLeft(this);
                    }
                    else
                    {
                        currentState = new GoriyaWalkUp(this);
                    }
                    break;
                case Direction.Up:
                    pos.Y += length;
                    if (previousDirection is Direction.Down)
                    {
                        currentState = new GoriyaWalkRight(this);
                    }
                    else
                    {
                        currentState = new GoriyaWalkDown(this);
                    }
                    break;
                case Direction.Left:
                    pos.X += length;
                    if (previousDirection is Direction.Right)
                    {
                        currentState = new GoriyaWalkUp(this);
                    }
                    else
                    {
                        currentState = new GoriyaWalkRight(this);
                    }
                    break;
                case Direction.Right:
                    pos.X -= length;
                    if (previousDirection is Direction.Left)
                    {
                        currentState = new GoriyaWalkDown(this);
                    }
                    else
                    {
                        currentState = new GoriyaWalkLeft(this);
                    }
                    break;
            }
        }
    }
}
