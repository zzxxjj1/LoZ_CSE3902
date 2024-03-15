using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class TrapMove : INPCState
    {
        private Trap trap;
        private LinkPlayer player;
        public ISprite sprite;
        private int currentDistance;

        bool isLinkGotten;

        public TrapMove(Trap trap)
        {
            this.trap = trap;
            sprite = NPCSpriteFactory.Instance.CreateTrapSprite();
            currentDistance = 0;
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Vector2 pos = new Vector2(trap.pos.X, trap.pos.Y);
            sprite.Draw(spriteBatch, pos);
        }

        public void Update()
        {
            if (!isLinkGotten)
            {
                if (!(trap.myGame.gameState is GamePlayState))
                    throw new InvalidOperationException(
                        "TrapMove: require GamePlayState");
                GamePlayState gameplay = (GamePlayState)trap.myGame.gameState;
                player = gameplay.player;
                isLinkGotten = true;
            }
            
            if (trap.going)
            {
                Going();
            }
            else if(!trap.returning)
            {
                Check();
            }
            else
            {
                Return();
            }

        }

        public void Check()
        {
            if ((player.GetPos().Y == trap.pos.Y) && (player.GetPos().X >= trap.pos.X))
            {
                trap.direction = Direction.Right;
                trap.going = true;
            }
            else if ((player.GetPos().Y == trap.pos.Y) && (player.GetPos().X <= trap.pos.X))
            {
                trap.direction = Direction.Left;
                trap.going = true;
            }
            else if ((player.GetPos().Y <= trap.pos.Y) && (player.GetPos().X == trap.pos.X))
            {
                trap.direction = Direction.Up;
                trap.going = true;
            }
            else if ((player.GetPos().Y >= trap.pos.Y) && (player.GetPos().X == trap.pos.X))
            {
                trap.direction = Direction.Down;
                trap.going = true;
            }
        }

        public void Going()
        {
            switch (trap.direction)
            {
                case Direction.Right:
                    trap.pos.X += trap.GoingVelocity;
                    currentDistance += trap.GoingVelocity;
                    if (currentDistance >= trap.MaxDistX)
                    {
                        trap.going = false;
                        trap.returning = true;
                    }
                    break;
                case Direction.Left:
                    trap.pos.X -= trap.GoingVelocity;
                    currentDistance += trap.GoingVelocity;
                    if (currentDistance >= trap.MaxDistX)
                    {
                        trap.going = false;
                        trap.returning = true;
                    }
                    break;
                case Direction.Down:
                    trap.pos.Y += trap.GoingVelocity;
                    currentDistance += trap.GoingVelocity;
                    if (currentDistance >= trap.MaxDistY)
                    {
                        trap.going = false;
                        trap.returning = true;
                    }
                    break;
                case Direction.Up:
                    trap.pos.Y -= trap.GoingVelocity;
                    currentDistance += trap.GoingVelocity;
                    if (currentDistance >= trap.MaxDistY)
                    {
                        trap.going = false;
                        trap.returning = true;
                    }
                    break;
            }
        }

        public void Return()
        {
            switch (trap.direction)
            {
                case Direction.Right: //Need back to left
                    trap.pos.X -= trap.ReturningVelocity;
                    currentDistance -= trap.ReturningVelocity;
                    if (currentDistance <= 0)
                    {
                        trap.returning = false;
                    }
                    break;
                case Direction.Left:
                    trap.pos.X += trap.ReturningVelocity;
                    currentDistance -= trap.ReturningVelocity;
                    if (currentDistance <= 0)
                    {
                        trap.returning = false;
                    }
                    break;
                case Direction.Down:
                    trap.pos.Y -= trap.ReturningVelocity;
                    currentDistance -= trap.ReturningVelocity;
                    if (currentDistance <= 0)
                    {
                        trap.returning = false;
                    }
                    break;
                case Direction.Up:
                    trap.pos.Y += trap.ReturningVelocity;
                    currentDistance -= trap.ReturningVelocity;
                    if (currentDistance <= 0)
                    {
                        trap.returning = false;
                    }
                    break;
            }
        }

        public void TakeDamage()
        {
        }
    }
}
