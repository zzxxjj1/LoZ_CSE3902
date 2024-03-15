using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoZ_CSE3902
{
    public static class HitBox
    {
        public static bool showBoarder = false;

        public static readonly Rectangle Empty = Rectangle.Empty;

        public static readonly Point Tile = new Point(16, 16);
        public static readonly Point TileOffset = new Point(-1);

        public static class NPC
        {
            public static readonly Point Boss = new Point(24, 32);
            public static readonly Point OldMan = new Point(16, 16);
            public static readonly Point FireBall = new Point(9, 10);
            public static readonly Point Gel = new Point(8, 9);
            public static readonly Point Kesse = new Point(16, 10);
            public static readonly Point Stalfos = new Point(16, 16);
            public static readonly Point Trap = new Point(16, 16);
            public static readonly Point WallMaster = new Point(16, 16);
            public static readonly Point GoriyaFront = new Point(14, 16);
            public static readonly Point GoriyaSide = new Point(15, 16);
            public static readonly Point GoriyaBoomerang = new Point(8, 8);
            public static readonly Point ExplosionEffect = new Point(16, 16);
        }
        public static class Item
        {
            public static readonly Point Arrow = new Point(5, 16);
            public static readonly Point Bomb = new Point(8, 14);
            public static readonly Point Boomerang = new Point(5, 9);
            public static readonly Point Bow = new Point(8, 16);
            public static readonly Point Clock = new Point(11, 16);
            public static readonly Point Compass = new Point(11, 12);
            public static readonly Point Fairy = new Point(8, 16);
            public static readonly Point Heart = new Point(7, 8);
            public static readonly Point HeartContainer = new Point(13, 12);
            public static readonly Point Key = new Point(8, 16);
            public static readonly Point Map = new Point(8, 16);
            public static readonly Point Rupee = new Point(8, 16);
            public static readonly Point Sword = new Point(7, 16);
            public static readonly Point Triforce = new Point(10, 10);
            public static readonly Point WhiteSword = new Point(7, 16);
        }
        public static class Link
        {
            public static readonly Point Body = new Point(16, 16);

            public static readonly Point Sword = new Point(8, 16);
            public static readonly Point SwordUp = new Point(8, 16);
            public static readonly Point SwordSide = new Point(16, 8);
            public static readonly Point Arrow = new Point(16, 16);
            public static readonly Point BombExplode = new Point(8, 14);
            public static readonly Point Boomerang = new Point(8, 9);
        }

        public static class Room
        {
            // please do not do any modification on Room's Hitboxes.
            // consider modify others first!
            public static readonly Point DoorUp = new Point(32, 16);
            public static readonly Point DoorSide = new Point(16, 32);
            public static readonly Point DoorClosed = new Point(32, 32);

            public static readonly Point DoorRightOffset = new Point(20, 0);
            public static readonly Point DoorDownOffset = new Point(0, 20);

            public static readonly Point WallPieceUp = new Point(80, 32);
            public static readonly Point[] WallPosUp = new Point[4]
            {
                new Point(32, 0),
                new Point(144, 0),
                new Point(32, 144),
                new Point(144, 144)
            };
            public static readonly Point WallPieceSide = new Point(32, 40);
            public static readonly Point[] WallPosSide = new Point[4]
            {
                new Point(0, 32),
                new Point(0, 104),
                new Point(224, 32),
                new Point(224, 104)
            };
        }


        public static Rectangle Create(Point pos, Point size)
        {
            return new Rectangle(pos, size);
        }

        // Use this overload to deal with misplaced hitbox
        // offset:  a positive value to scale up (make hit box bigger relative to the CENTER).
        public static Rectangle Create(Point pos, Point size, Point offset)
        {
            return new Rectangle(pos - offset, size + offset + offset);
        }

        // posOffsetFlag: if true, only apply the offset to pos (add offset from pos)
        public static Rectangle Create(Point pos, Point size, Point offset, bool posOffsetFlag)
        {
            if (posOffsetFlag)
                return new Rectangle(pos + offset, size);
            else
                return Create(pos, size, offset);
        }


        // a demo overload to retrieve field by string identifiers
        public static Rectangle Create(Point pos, string genre, string name, Point offset)
        {
            var genreClass = typeof(HitBox).GetField(genre).GetValue(null);
            var objClass = genreClass.GetType().GetField(name).GetValue(null);
            return Create(pos, (Point)objClass, offset);
        }




    }
}
