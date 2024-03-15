using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoZ_CSE3902
{
    public class Exterior
    {
        public Vector2 pos;
        private ISprite sprite;
        public Door[] door;
        public List<WallPiece> wallList;

        public Exterior(Dictionary<Direction, DoorState> doorData)
        {
            pos = new Vector2(0, 0);
            sprite = TilesSpriteFactory.Instance.CreateExteriorSprite();

            door = new Door[4];
            foreach (var pair in doorData)
            {
                door[(int)pair.Key] = new Door(pair.Key, pair.Value);
            }

            wallList = new List<WallPiece>();
            foreach (Point pos in HitBox.Room.WallPosUp)
            {
                wallList.Add(new WallPiece(pos.ToVector2(), Direction.Up));
            }
            foreach (Point pos in HitBox.Room.WallPosSide)
            {
                wallList.Add(new WallPiece(pos.ToVector2(), Direction.Right));
            }
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, pos);
            foreach (var d in door)
            {
                d.Draw(spriteBatch);
            }

            if (HitBox.showBoarder)
                foreach (var wall in wallList)
                {
                    GameUtility.Instance.DrawBoarder(wall.GetRectangle(), Color.MediumSpringGreen, 1);
                }
        }

        public List<ICollider> GetColliderList()
        {
            List<ICollider> collidables = new List<ICollider>();
            foreach (Door x in door)
                collidables.Add((ICollider)x);
            foreach (WallPiece x in wallList)
                collidables.Add((ICollider)x);

            return collidables;
        }
        public void SetDoorState(Direction direction, DoorState state)
        {
            door[(int)direction].SetState(state);
        }
        public bool IsDoorOpen(Direction direction)
        {
            return door[(int)direction].IsOpen();
        }
        public void OpenClosedDoors()
        {
            foreach (Door d in door)
            {
                d.Open();
            }
        }
        public void Transit(Vector2 offset)
        {
            pos += offset;

            foreach (Door d in door)
            {
                d.Transit(offset);
            }
        }
    }
}
