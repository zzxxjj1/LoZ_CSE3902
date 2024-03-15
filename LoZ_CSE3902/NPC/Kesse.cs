using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Kesse : INPC
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
        public int framePerStep = 5;

        public Kesse(Vector2 pos, Game1 game)
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

        /* Random choose a direction */
        public void RandomDirection()
        {
            randomNum = random.Next(1, 8);

            switch (randomNum)
            {
                case 1:
                    currentState = new KesseWalkUp(this);
                    break;
                case 2:
                    currentState = new KesseWalkDown(this);
                    break;
                case 3:
                    currentState = new KesseWalkLeft(this);
                    break;
                case 4:
                    currentState = new KesseWalkRight(this);
                    break;
                case 5:
                    currentState = new KesseWalkUpLeft(this);
                    break;
                case 6:
                    currentState = new KesseWalkUpRight(this);
                    break;
                case 7:
                    currentState = new KesseWalkDownLeft(this);
                    break;
                case 8:
                    currentState = new KesseWalkDownRight(this);
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
            return HitBox.Create(pos.ToPoint(), HitBox.NPC.Kesse);
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
                    currentState = new KesseWalkUp(this);
                    break;
                case Direction.Up:
                    pos.Y += length;
                    currentState = new KesseWalkDown(this);
                    break;
                case Direction.Left:
                    pos.X += length;
                    currentState = new KesseWalkRight(this);
                    break;
                case Direction.Right:
                    pos.X -= length;
                    currentState = new KesseWalkLeft(this);
                    break;
            }
        }

    }
}
   
