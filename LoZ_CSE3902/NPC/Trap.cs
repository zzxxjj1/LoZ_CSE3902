using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class Trap : INPC
    {
        public Game1 myGame;
        public INPCState currentState;
        public Vector2 pos;
        public Vector2 Pos
        {
            get { return pos; }
            set { if (pos != null) pos = value; }
        }
        public bool IsAlive { get; set; } = true; // trap never die

        public Vector2 initialPos;
        public Direction direction;
        public Boolean going, returning;
        public int MaxDistX = GameAttributes.Window.TileLayoutUnderWorld.X * GameAttributes.Window.TileWidth / 2;
        public int MaxDistY = GameAttributes.Window.TileLayoutUnderWorld.Y * GameAttributes.Window.TileHeight / 2;
        public int GoingVelocity = 2;
        public int ReturningVelocity = 1;


        public Trap(Vector2 pos, Game1 game)
        {
            this.pos = pos;
            this.initialPos = pos;
            this.myGame = game;
            currentState = new TrapMove(this);
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
            return HitBox.Create(pos.ToPoint(), HitBox.NPC.Trap);
        }

        public void TakeDamage(Direction side)
        {
        }

        public void ChangeDirection(float length, Direction side)
        {
            going = false;
            returning = true;
        }

        public void Update()
        {
            currentState.Update();
        }

    }
}
