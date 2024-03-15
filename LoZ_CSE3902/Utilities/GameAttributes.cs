using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoZ_CSE3902
{
    public static class GameAttributes
    {
        public static bool Paused = false;

        public static class Window
        {
            public const int ScalingFactor = 3;
            public static readonly Matrix ScalingMatrix =
                Matrix.CreateScale(ScalingFactor);

            // nes resolution on a standard NTSC TV 
            public const int NTSCResolutionWidth = 256;
            //public const int NTSCResolutionHeight = 224;
            public const int NTSCResolutionHeight = 232;
            // no idea why do we have these 8 pixels left here.

            public static readonly Point TileLayoutOverWorld = new Point(16, 11);
            public static readonly Point TileLayoutUnderWorld = new Point(12, 7);

            public const int HUDBarWidth = NTSCResolutionWidth;
            public const int HUDBarHeight = 56;
            public static readonly Rectangle HUDBarOffset =
                new Rectangle(0, 56, 0, 0);
            // nums, mini-map rooms, hearts, etc. -- 8x8px
            public const int TokenWidth = 8, TokenHeight = 8;

            // tiles, blocks, or link -- 16x16px
            public const int TileWidth = 16, TileHeight = 16;

            public static readonly int InGameWidth = TileLayoutOverWorld.X * TileWidth;
            public static readonly int InGameHeight = TileLayoutOverWorld.Y * TileHeight;

            public static readonly Point ItemTokenSize = new Point(8, 16);

            public static class Pos
            {
                public static readonly Vector2 RupeeCount = new Vector2(96, 16);
                public static readonly Vector2 KeyCount = new Vector2(96, 32);
                public static readonly Vector2 BombCount = new Vector2(96, 40);
                public static readonly Vector2 HeartDisplay = new Vector2(176, 32);
                public static readonly Vector2 MiniMapDisplay = new Vector2(16, 8);
                public static readonly Vector2 ItemA = new Vector2(152, 24);
                public static readonly Vector2 ItemB = new Vector2(128, 24);
            }
        }
        public static class Player
        {
            // change by factor
            public const int Width = 15, Height = 16;
            public const float WalkSpeed = 1.0f;
            public const int FramesPerStep = 6;
            public const int FramesToSwingSword = 15;
            public const int FramesToUseItem = 10;
            public const int SlideByDamage = 40;

            public const int StartMaxHealth = 3 * 2; // 6 half-heart == 3 full-heart
            public const int HealthLimit = 16 * 2;

            public const int MaxRupee = 255;
            public const int MaxBomb = 8;
            public const int MaxKey = 8;

            public static class Pos
            {
                public static readonly Vector2 StartPos = new Vector2(5.5f, 6);

                // index from 0 to 3: up, left, right, down
                public static readonly Vector2[] AfterRoomTransit = new Vector2[]
                {
                    // when you go up and transit to a room at north, you will be at this pos.
                    new Vector2(5.5f, 6), 
                    new Vector2(11, 3), // etc.
                    new Vector2(0, 3),
                    new Vector2(5.5f, 0)
                };
                public static readonly Point DoorSide = new Point(16, 32);
                public static readonly Point DoorClosed = new Point(32, 32);
            }

        }

        public static class Room
        {
            //public static readonly int TransitionFrames = 90;
            public const float TransitionSpeed = 3.0f; // pixel per frame

            public static readonly Rectangle OverworldBoundary = 
                new Rectangle(0, Window.HUDBarHeight, Window.InGameWidth, Window.InGameHeight);
            public static readonly Rectangle UnderworldBoundary =
                new Rectangle(2*16, 2*16 + Window.HUDBarHeight, 12*16, 7*16);
        }






    }
}
