using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class WallMaster : INPC
    {
        public INPCState currentState;
        public Vector2 pos;
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        public bool IsAlive { get; set; } = true;
        public Direction previousDirection, currentDirection, spriteDirection;
        public Game1 myGame;
        public int framePerStep = 5;
        public int health;
        int randomNum;
        public System.Random random;
        public Boolean going;
        public int MinRoomX = 2 * GameAttributes.Window.TileWidth;
        public int MaxRoomX = 2 * GameAttributes.Window.TileWidth +
            GameAttributes.Window.TileLayoutUnderWorld.X * GameAttributes.Window.TileWidth - HitBox.NPC.WallMaster.X;
        public int MinRoomY = 2 * GameAttributes.Window.TileHeight;
        public int MaxRoomY = 2 * GameAttributes.Window.TileHeight +
            GameAttributes.Window.TileLayoutUnderWorld.Y * GameAttributes.Window.TileHeight - HitBox.NPC.WallMaster.Y;

        // You will need the boundary of the room to determine which way this wallmaster is moving.
        // We can keep the constructor simple. --Yuhan

        public WallMaster(Vector2 pos, Game1 game)
        {
            this.pos = pos;
            this.myGame = game;
            this.health = 2;
            random = new Random();
            this.RandomSprite();
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
                    if(previousDirection is Direction.Up)
                    {
                        currentState = new WallMasterWalkLeft(this);
                    }
                    else
                    {
                        currentState = new WallMasterWalkUp(this);
                    }
                    break;
                case Direction.Up:
                    pos.Y += length;
                    if(previousDirection is Direction.Down)
                    {
                        currentState = new WallMasterWalkRight(this);
                    }
                    else
                    {
                        currentState = new WallMasterWalkDown(this);
                    }
                    break;
                case Direction.Left:
                    pos.X += length;
                    if(previousDirection is Direction.Right)
                    {
                        currentState = new WallMasterWalkUp(this);
                    }
                    else
                    {
                        currentState = new WallMasterWalkRight(this);
                    }
                    break;
                case Direction.Right:
                    pos.X -= length;
                    if (previousDirection is Direction.Left)
                    {
                        currentState = new WallMasterWalkDown(this);
                    }
                    else
                    {
                        currentState = new WallMasterWalkLeft(this);
                    }
                    break;
            }
        }

        public Rectangle GetRectangle()
        {
            if (currentState is Death) return HitBox.Empty;
            return HitBox.Create(pos.ToPoint(), HitBox.NPC.WallMaster);
        }


        public Vector2 GetPos()
        {
            return pos;
        }

        public void RandomSprite()
        {
            randomNum = random.Next(1, 4);

            switch (randomNum)
            {
                case 1:
                    this.spriteDirection = Direction.DownLeft;
                    break;
                case 2:
                    this.spriteDirection = Direction.DownRight;
                    break;
                case 3:
                    this.spriteDirection = Direction.UpLeft;
                    break;
                case 4:
                    this.spriteDirection = Direction.UpRight;
                    break;
            }
        }

        public void RandomDirection()
        {
            randomNum = random.Next(1, 4);
            switch (randomNum)
            {
                case 1:
                    currentState = new WallMasterWalkUp(this);
                    break;
                case 2:
                    currentState = new WallMasterWalkDown(this);
                    break;
                case 3:
                    currentState = new WallMasterWalkLeft(this);
                    break;
                case 4:
                    currentState = new WallMasterWalkRight(this);
                    break;
            }
        }

    }
}
