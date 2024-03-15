using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Fireball : INPC
    {
        public INPCState currentState;
        public Vector2 pos;
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        public Boolean fire;
        public int position;
        // 1 is the fireball on the top, 2 is in the middle, and 3 is at the bottom
        public int framePerStep = 7;
        public bool IsAlive { get; set; }
        public Game1 myGame;

        public Fireball(float xPos, float yPos, Boolean fire, int position, Game1 game)
        {
            this.pos.X = xPos;           
            this.pos.Y = yPos;
            this.fire = fire;
            this.position = position;
            IsAlive = true;
            this.myGame = game;
            currentState = new FireballMove(this, xPos, yPos, fire, position,myGame);
        }
        public void Update()
        {
            currentState.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentState.Draw(spriteBatch, pos.X, pos.Y);
        }

        public void TakeDamage(Direction side)
        {
        }

        public Rectangle GetRectangle()
        {
            return HitBox.Create(pos.ToPoint(), HitBox.NPC.FireBall);
        }

        public Vector2 GetPos()
        {
            return pos;
        }

        public void ChangeDirection(float length, Direction side)
        {
        }
    }
}
