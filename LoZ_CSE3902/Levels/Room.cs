using LoZ_CSE3902;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoZ_CSE3902
{
    public class Room
    {
        public RoomData data { get; }
        public Exterior exterior;
        public LinkPlayer player;

        // these entities should only live in a Room instance
        private List<IItem> itemsOnFloor;
        private List<ITile> tiles;
        private List<INPC> NPCs;
        private List<ICollider> toBeDeleted;
        

        public static Vector2 ConvertGridToScreenPosition(Vector2 pos, bool isInUnderworld)
        {
            if (isInUnderworld)
            {
                pos.X += 2;
                pos.Y += 2;
                //Debug.Print("GetPosOnScreen: Underworld, Pos({0}, {1}) (Room-Static)", pos.X, pos.Y);
            }

            if ((pos.X <= 0 && pos.Y <= 0) && (pos.X >= 16 && pos.Y >= 9))
            {
                Debug.Print("GetPosOnScreen: invalid pos({0}, {1}). (Room-Static)", pos.X, pos.Y);
                throw new InvalidOperationException("Invalid pos.");
            }
            
            // unit of object pos when initializing: 16*16px
            // convert object pos to pixel pos
            pos.X *= 16;
            pos.Y *= 16;

            return pos;
        }

        public Room(LinkPlayer player, List<ITile> tileList, List<IItem> itemList,
            List<INPC> npcList, RoomData data)
        {
            this.player = player;
            this.data = data;
            itemsOnFloor = itemList;
            tiles = tileList;
            NPCs = npcList;
            toBeDeleted = new List<ICollider>();
            if (this.data.InUnderworld)
            {
                exterior = new Exterior(this.data.DoorData);
            }

            Debug.Print("Room: Room {0} created in Level {1}.", data.ID, data.Level);
        }

        public void Update()
        {
            foreach (INPC npc in NPCs.ToArray())
            {
                if (!npc.IsAlive)
                    toBeDeleted.Add(npc);
                else npc.Update();
            }
            foreach (ITile tile in tiles)
            {
                tile.Update();
            }
            foreach (IItem item in itemsOnFloor)
            {
                if (item.IsPicked())
                    toBeDeleted.Add(item);
                else item.Update();
            }

            RemoveObjectProcess(toBeDeleted);
            toBeDeleted = new List<ICollider>();

            // only checks number of NPCs to open doors
            if (NPCs.Count == 0)
            {
                exterior.OpenClosedDoors();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ITile x in tiles) { x.Draw(spriteBatch); }
            exterior.Draw(spriteBatch);
            
            foreach (IItem x in itemsOnFloor) { x.Draw(spriteBatch); }
            foreach (INPC x in NPCs) { x.Draw(spriteBatch); }

            if (HitBox.showBoarder)
            {
                foreach (ITile x in tiles)
                    if (x.IsObstacle())
                        GameUtility.Instance.DrawBoarder(x.GetRectangle(), Color.Gray, 1);
                foreach (IItem x in itemsOnFloor)
                    GameUtility.Instance.DrawBoarder(x.GetRectangle(), Color.Orange, 1);
                foreach (INPC x in NPCs)
                    GameUtility.Instance.DrawBoarder(x.GetRectangle(), Color.LightSalmon, 1);
            }
        }
        public List<ICollider> GetColliderList()
        {
            List<ICollider> collidables = new List<ICollider>();

            collidables.AddRange(exterior.GetColliderList());

            foreach (ITile x in tiles)
                if (x.IsObstacle()) collidables.Add((ICollider)x);
            foreach (IItem x in itemsOnFloor)
                collidables.Add((ICollider)x);
            foreach (INPC x in NPCs)
                collidables.Add((ICollider)x);

            return collidables;
        }

        public bool IsDoorOpen(Direction direction)
        {
            return exterior.IsDoorOpen(direction);
        }

        public void DeleteEntity(ICollider collider)
        {
            if (collider is INPC)
                NPCs.Remove((INPC)collider);
            
            if (collider is IItem)
                itemsOnFloor.Remove((IItem)collider);
            
            if (collider is ITile)
                tiles.Remove((ITile)collider);
        }

        public void AddEntity(ICollider collider)
        {
            if (collider is INPC)
                NPCs.Add((INPC)collider);

            if (collider is IItem)
                itemsOnFloor.Add((IItem)collider);

            if (collider is ITile)
                tiles.Add((ITile)collider);
        }

        public void RemoveObjectProcess(List<ICollider> colliderList)
        {
            foreach (ICollider collider in colliderList)
            {
                DeleteEntity(collider);
                if (collider is INPC)
                {
                    INPC npc = (INPC)collider;
                    if (collider is Boss)
                    {
                        itemsOnFloor.Add(new HeartContainer(player, ((Boss)npc).pos));
                        continue;
                    }
                    if (collider is Fireball) continue;
                    if (collider is GoriyaBoomerang) continue;
                    DropItem(npc.Pos);
                }
            }
        }

        public void DropItem(Vector2 pos)
        {
            Random randomMachine = new Random();
            int randomNum = randomMachine.Next(1, 16);
            switch (randomNum)
            {
                case 1:
                case 2:
                    itemsOnFloor.Add(new Heart(player, pos));
                    break;
                case 4:
                case 5:
                    itemsOnFloor.Add(new Rupee(player, pos));
                    break;
                case 8:
                    itemsOnFloor.Add(new Bomb(player, pos));
                    break;
                default:
                    break;
            }
        }

        public void DrawForTransition(SpriteBatch spriteBatch)
        {
            foreach (ITile x in tiles) { x.Draw(spriteBatch); }
            exterior.Draw(spriteBatch);
        }
    }
}
