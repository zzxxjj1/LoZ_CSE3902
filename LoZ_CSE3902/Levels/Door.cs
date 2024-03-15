using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoZ_CSE3902
{
    public class Door : ICollider
    {
        public Vector2 pos;
        private ISprite sprite;
        public DoorState state;
        public Direction direction;
        private Rectangle hitbox;
        public Door(Direction direction, DoorState state)
        {
            this.direction = direction;
            SetPos(direction);
            SetState(state);
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, pos);
            if (HitBox.showBoarder)
                GameUtility.Instance.DrawBoarder(GetRectangle(), Color.BlueViolet, 1);
        }

        public Rectangle GetRectangle()
        {
            return hitbox;
        }

        public void SetState(DoorState state)
        {
            this.state = state;
            sprite = TilesSpriteFactory.Instance.CreateDoorSprite(direction, state);

            if (state == DoorState.Wall || state == DoorState.Locked ||
                state == DoorState.Closed || state == DoorState.Destructable)
            {
                hitbox = HitBox.Create(pos.ToPoint(), HitBox.Room.DoorClosed);
                return;
            }

            switch (direction)
            {
                case Direction.Up:
                    hitbox = HitBox.Create(pos.ToPoint(), HitBox.Room.DoorUp);
                    return;
                case Direction.Left:
                    hitbox = HitBox.Create(pos.ToPoint(), HitBox.Room.DoorSide);
                    return;
                case Direction.Right:
                    hitbox =  HitBox.Create(pos.ToPoint(), 
                        HitBox.Room.DoorSide, HitBox.Room.DoorRightOffset, true);
                    return;
                case Direction.Down:
                    hitbox = HitBox.Create(pos.ToPoint(), 
                        HitBox.Room.DoorUp, HitBox.Room.DoorDownOffset, true);
                    return;
                default:
                    throw new InvalidOperationException("GetRectangle: Invalid direction. (Door)");
            }
        }

        public Rectangle SetPos(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    pos = new Vector2(112, 0);
                    break;
                case Direction.Left:
                    pos = new Vector2(0, 72);
                    break;
                case Direction.Right:
                    pos = new Vector2(224, 72);
                    break;
                case Direction.Down:
                    pos = new Vector2(112, 144);
                    break;
            }
            return new Rectangle(pos.ToPoint(), new Point(32, 32));
        }

        public bool IsOpen()
        {
            bool isOpen = state.Equals(DoorState.Open) || state.Equals(DoorState.Destructed);
            Debug.Print("IsOpen: {0} at {1}, State is {2}", isOpen, direction, state);
            return isOpen;
        }

        public bool Unlock(LinkPlayer player)
        {
            bool isUnlocked = false;
            if (state == DoorState.Locked)
                if (isUnlocked = player.UseKey())
                {
                    SoundManager.Instance.Play(SoundEnum.Door_Unlock);
                    SetState(DoorState.Open);
                }
            return isUnlocked;
        }

        public bool Destruct()
        {
            bool isDestructed = false;
            if (state == DoorState.Destructable)
                SetState(DoorState.Destructed);
            return isDestructed;
        }
        public void Open()
        {
            if (state == DoorState.Closed)
            {
                SoundManager.Instance.Play(SoundEnum.Door_Unlock);
                SetState(DoorState.Open);
            }
        }

        public void Transit(Vector2 offset)
        {
            pos += offset;
        }
    }
}
