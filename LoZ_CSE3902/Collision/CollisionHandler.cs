using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;


namespace LoZ_CSE3902
{
    public class CollisionHandler
    {
        private Room room;
        private Game1 game;
        private Dictionary<Tuple<Type, Type, Direction>, Type> collisionMap;
        private HashSet<Tuple<Type, Type, Direction>> availableHandlers;

        public CollisionHandler(Game1 game, Room room)
        {
            this.game = game;
            this.room = room;

            collisionMap = new Dictionary<Tuple<Type, Type, Direction>, Type>();
            CreateCollisionMap();
            availableHandlers = new HashSet<Tuple<Type, Type, Direction>>(collisionMap.Keys);
        }

        public enum AvailableInterface
        {
            ITile,
            INPC,
            IItem,
            IGadgetEffect
        }

        private void CreateCollisionMap()
        {
            Type playerType = typeof(LinkPlayer);
            Type wallType = typeof(WallPiece);
            Type doorType = typeof(Door);
            Type cloneType = typeof(LinkClone);

            var tileType = typeof(ITile);
            var NPCType = typeof(INPC);
            var itemType = typeof(IItem);
            var gadgetEffectType = typeof(IGadgetEffect);

            foreach (Direction side in Enum.GetValues(typeof(Direction)))
            {
                // Case 1: mainObj is player
                collisionMap.Add(new Tuple<Type, Type, Direction>(playerType, doorType, side), typeof(PlayerPushDoor));
                collisionMap.Add(new Tuple<Type, Type, Direction>(playerType, wallType, side), typeof(PlayerBlockingMovement));
                collisionMap.Add(new Tuple<Type, Type, Direction>(playerType, tileType, side), typeof(PlayerBlockingMovement));
                collisionMap.Add(new Tuple<Type, Type, Direction>(playerType, itemType, side), typeof(PlayerPickUp));
                collisionMap.Add(new Tuple<Type, Type, Direction>(playerType, NPCType, side), typeof(PlayerTakeDamage));

                // Case 2: mainObj is INPC
                collisionMap.Add(new Tuple<Type, Type, Direction>(NPCType, wallType, side), typeof(NPCBlockingMovement));
                collisionMap.Add(new Tuple<Type, Type, Direction>(NPCType, doorType, side), typeof(NPCBlockingMovement));
                collisionMap.Add(new Tuple<Type, Type, Direction>(NPCType, tileType, side), typeof(NPCBlockingMovement));
                collisionMap.Add(new Tuple<Type, Type, Direction>(NPCType, typeof(SwordHead), side), typeof(NPCTakeDamage));
                collisionMap.Add(new Tuple<Type, Type, Direction>(NPCType, typeof(BombPlaced), side), typeof(NPCTakeDamage));
                collisionMap.Add(new Tuple<Type, Type, Direction>(NPCType, typeof(ArrowShooted), side), typeof(NPCTakeDamage));
                collisionMap.Add(new Tuple<Type, Type, Direction>(NPCType, typeof(SwordShooted), side), typeof(NPCTakeDamage));

                // Case 3: mainObj is Gadget Effects
                //collisionMap.Add(new Tuple<Type, Type, Direction>(gadgetEffectType, itemType, side), typeof(ItemPickUpByGadget));
                collisionMap.Add(new Tuple<Type, Type, Direction>(typeof(ArrowShooted), doorType, side), typeof(GadgetBlockingDisappear));
                collisionMap.Add(new Tuple<Type, Type, Direction>(typeof(ArrowShooted), NPCType, side), typeof(GadgetBlockingDisappear));
                collisionMap.Add(new Tuple<Type, Type, Direction>(typeof(SwordShooted), NPCType, side), typeof(GadgetBlockingDisappear));
                collisionMap.Add(new Tuple<Type, Type, Direction>(typeof(BombPlaced), doorType, side), typeof(BombDestroyDoor));

                // Case 4: mainObj is LinkClone
                collisionMap.Add(new Tuple<Type, Type, Direction>(cloneType, doorType, side), typeof(CloneBlockingMovement));
                collisionMap.Add(new Tuple<Type, Type, Direction>(cloneType, wallType, side), typeof(CloneBlockingMovement));
                collisionMap.Add(new Tuple<Type, Type, Direction>(cloneType, tileType, side), typeof(CloneBlockingMovement));
                collisionMap.Add(new Tuple<Type, Type, Direction>(cloneType, itemType, side), typeof(ClonePickUp));
                collisionMap.Add(new Tuple<Type, Type, Direction>(cloneType, NPCType, side), typeof(CloneTakeDamage));
            }
        }

        public ICommand getCommand(ICollider mainObj, ICollider collider, Direction side, Type commandType)
        {
            Type mainObjType = mainObj.GetType();
            Type colliderType = collider.GetType();

            List<Type[]> signatures = new List<Type[]>();
            signatures.Add(new Type[] { mainObjType, colliderType, typeof(Direction) });

            ConstructorInfo commandConstructor = null;
            foreach (Type[] signature in signatures)
            {
                commandConstructor = commandType.GetConstructor(signature);
                if (commandConstructor != null) { break; }
            }
            if (commandConstructor == null) { return null; }

            switch (commandConstructor.GetParameters().Length)
            {
                case 3:
                    //If the third parameter is Direction
                    if (commandConstructor.GetParameters()[2].ParameterType == typeof(Direction))
                    {
                        return (ICommand)commandConstructor.Invoke(new object[] { mainObj, collider, side });
                    }
                    else //Else the third parameter is Room
                    {
                        return (ICommand)commandConstructor.Invoke(new object[] { mainObj, collider, room });
                    }
                default:
                    throw new InvalidOperationException("Unsupported handlers.");
            }
        }

        private Type GetInterfaceOrType(ICollider obj)
        {
            Type supportType = obj.GetType();

            if (obj is LinkPlayer) supportType = typeof(LinkPlayer);
            else if (obj is LinkClone) supportType = typeof(LinkClone);
            else if (obj is WallPiece) supportType = typeof(WallPiece);
            else if (obj is Door) supportType = typeof(Door);
            else if (obj is ITile) supportType = typeof(ITile);
            else if (obj is INPC) supportType = typeof(INPC);
            else if (obj is IItem) supportType = typeof(IItem);
            else if (obj is ArrowShooted) supportType = typeof(ArrowShooted);
            else if (obj is SwordHead) supportType = typeof(SwordHead);
            else if (obj is BombPlaced) supportType = typeof(BombPlaced);

            return supportType;
        }

        public void Handle(ICollider mainObj, ICollider collider, Direction side)
        {
            Type object1Type = GetInterfaceOrType(mainObj);
            Type object2Type = GetInterfaceOrType(collider);

            Tuple<Type, Type, Direction> key = new Tuple<Type, Type, Direction>(object1Type, object2Type, side);
            if (availableHandlers.Contains(key))
            {
                Type commandType = collisionMap[key];
                //Debug.Print("Handle collision: {0}", commandType);
                Debug.Print("Handle： {0} hits {1} on {2}, execute {3}", mainObj, collider, side, commandType);
                ICommand command = getCommand(mainObj, collider, side, commandType);
                if (command != null) { command.Execute(); }
            }
        }

        public static float GetPenetrationLength(ICollider mainObj, ICollider collider, Direction side)
        {
            float length = 100;
            Rectangle mainRec = mainObj.GetRectangle();
            Rectangle collRec = collider.GetRectangle();
            switch (side)
            {
                case Direction.Up:
                    length = collRec.Y + collRec.Height - mainRec.Y;
                    break;
                case Direction.Left:
                    length = collRec.X + collRec.Width - mainRec.X;
                    break;
                case Direction.Right:
                    length = mainRec.X + mainRec.Width - collRec.X;
                    break;
                case Direction.Down:
                    length = mainRec.Y + mainRec.Height - collRec.Y;
                    break;
            }
            return length;
        }


    }

}
