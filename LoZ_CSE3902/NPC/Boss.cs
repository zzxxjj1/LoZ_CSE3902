using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Boss : INPC
    {
        public INPCState currentState;
        public Vector2 pos;
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        public bool IsAlive { get; set; } = true;

        public Game1 myGame;
        public bool left;
        public int framePerStep = 10;
        public int health;
        Fireball fireball1, fireball2, fireball3;

        // add a bool to indicate if this is alive
        // room will check this and delete the enemy if dead

        public Boss(Vector2 pos, Game1 game)
        {
            this.myGame = game;
            this.pos = pos;
            this.left = true;
            this.health = 6;
            fireball1 = new Fireball(this.pos.X - 10, this.pos.Y, true, 1, myGame);
            fireball2 = new Fireball(this.pos.X - 10, this.pos.Y + 20, true, 2, myGame);
            fireball3 = new Fireball(this.pos.X - 10, this.pos.Y + 40, true, 3, myGame);
            currentState = new BossWalkLeft(this, fireball1, fireball2, fireball3);
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
            if (currentState is Death) return HitBox.Empty;
            return HitBox.Create(pos.ToPoint(), HitBox.NPC.Boss);
        }

        public void TakeDamage(Direction side)
        {
            currentState.TakeDamage();
        }

        public void Update()
        {
            currentState.Update();
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
                    currentState = new BossWalkLeft(this, this.fireball1, this.fireball2, this.fireball3);
                    break;
                case Direction.Right:
                    pos.X -= length;
                    currentState = new BossWalkRight(this, this.fireball1, this.fireball2, this.fireball3);
                    break;
            }
        }
    }
}
