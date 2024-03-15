using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoZ_CSE3902
{
    public class CollisionDetector
    {
        private List<ICollider> colliderList;
        private List<ICollider> waitToBeDeleted;
        private LinkPlayer player;
        private Room room;
        private static CollisionHandler collisionHander;

        /*
        private readonly Type[] NonMovingType = new Type[]
        {
            typeof(ITile),
            typeof(WallPiece),
            typeof(Door)
        };
        */

        public CollisionDetector(Game1 game, Room room, LinkPlayer player)
        {
            this.room = room;
            this.player = player;
            collisionHander = new CollisionHandler(game, room);
            this.waitToBeDeleted = new List<ICollider>();
        }

        public void AddColliders(ICollider collider)
        {
            colliderList.Add(collider);
        }

        public void DeleteColliders(ICollider collider)
        {
            waitToBeDeleted.Add(collider);
        }

        public void DeleteNPC(ICollider collider)
        {
            room.DeleteEntity(collider);
        }

        public bool CheckCollided(Rectangle object1, Rectangle object2)
        {
            if (object1.Equals(HitBox.Empty) || object2.Equals(HitBox.Empty))
                return false;
            else
                return object1.Intersects(object2);
        }

        public Direction GetDirection(Rectangle object1, Rectangle object2)
        {
            float deltaX = object1.Center.X - object2.Center.X;
            float deltaY = object1.Center.Y - object2.Center.Y;
            Direction horizontal = deltaX > 0 ? Direction.Left : Direction.Right;
            Direction vertical = deltaY > 0 ? Direction.Up : Direction.Down;


            float horizontalGap = Math.Abs(deltaX) - (object1.Width / 2) - (object2.Width / 2);
            float verticalGap = Math.Abs(deltaY) - (object1.Height / 2) - (object2.Height / 2);

            float horizontalOverlap = Math.Max(0, -horizontalGap);
            float verticalOverlap = Math.Max(0, -verticalGap);

            if (horizontalOverlap > verticalOverlap)
            {
                return vertical;
            }
            else
            {
                return horizontal;
            }

        }

        public void Update()
        {
            colliderList = room.GetColliderList();
            colliderList.AddRange(player.GetColliderList());

            // mainobj should always be moving
            Rectangle mainObjRectangle, colliderRectangle;
            foreach (ICollider mainObj in colliderList)
            {
                if (mainObj is ITile) { continue; }
                if (mainObj is WallPiece) { continue; }
                if (mainObj is Door) { continue; }
                // more non-moving checks here

                foreach (ICollider collider in colliderList)
                {
                    if (collider == mainObj) { continue; }

                    mainObjRectangle = mainObj.GetRectangle();
                    colliderRectangle = collider.GetRectangle();

                    if (CheckCollided(mainObjRectangle, colliderRectangle))
                    {
                        Direction side = GetDirection(mainObjRectangle, colliderRectangle);
                        //Debug.Print("CDetector： {0} hits {1} on {2}", mainObj.GetType(), collider.GetType(), side);
                        collisionHander.Handle(mainObj, collider, side);
                    }
                }
            }
            foreach (ICollider collider in waitToBeDeleted)
            {
                colliderList.Remove(collider);
            }
        }
    }
}
