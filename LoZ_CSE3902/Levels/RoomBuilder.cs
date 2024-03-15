using LoZ_CSE3902;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace LoZ_CSE3902
{
    public static class RoomBuilder
    {
        private static readonly string NamespacePrefix = "LoZ_CSE3902.";
        public static Room Build(Game1 game, LinkPlayer player, int level, int id)
        {
            /*
            if (!(game.gameState is GamePlayState))
                throw new InvalidOperationException(
                    "RoomBuilder: Only build room in GamePlayState");
            GamePlayState gameplay = (GamePlayState)game.gameState;
            */

            List<IItem> ItemsOnFloor = new List<IItem>();
            List<ITile> Tiles = new List<ITile>();
            List<INPC> NPCs = new List<INPC>();

            RoomData roomData; // = GetRoomData(level, id);

            try
            {
                roomData = GetRoomData(level, id);
            }
            catch (FileNotFoundException)
            {
                return DevRoom(game, player);
            }
            
            int underworldOffset = 0;

            // a 2d-array to check if each place is covered by a tile
            bool[,] tilePlaced = new bool[16, 9];
            
            if (roomData.InUnderworld)
            {
                // disable boarder tiles in the underworld.
                underworldOffset = 2;
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (i < 2 || i > 13) tilePlaced[i, j] = true;
                        if (j < 2 || j > 9) tilePlaced[i, j] = true;
                    }
                }
            }

            foreach (var pair in roomData.ObjectDictionary)
            {
                Type resolvedType = Type.GetType(NamespacePrefix + pair.Key);
                object[] args;
                foreach (var pos in pair.Value)
                {
                    Vector2 posOnScreen = Room.ConvertGridToScreenPosition(
                        new Vector2(pos.X, pos.Y), roomData.InUnderworld);
                    if (Enum.IsDefined(typeof(TileInGame), pair.Key))
                    {
                        args = new object[] { posOnScreen };
                        object grabObj = (Activator.CreateInstance(resolvedType, args));
                        tilePlaced[pos.X + underworldOffset, pos.Y + underworldOffset] = true;
                        Tiles.Add((ITile)grabObj);
                    }
                    else if (Enum.IsDefined(typeof(ItemInGame), pair.Key))
                    {
                        args = new object[] { player, posOnScreen };
                        object grabObj = (Activator.CreateInstance(resolvedType, args));
                        ItemsOnFloor.Add((IItem)grabObj);
                    }
                    else if (Enum.IsDefined(typeof(NPCInGame), pair.Key))
                    {
                        args = new object[] { posOnScreen, game };
                        object grabObj = Activator.CreateInstance(resolvedType, args);
                        NPCs.Add((INPC)grabObj);
                    }
                    //Debug.Print("Build: '{0}' placed on ({1}, {2}). (RoomBuilder)", resolvedType, pos.X, pos.Y);
                }
            }

            // filling with default tiles
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!tilePlaced[i, j])
                    {
                        Type resolvedType = Type.GetType(NamespacePrefix + roomData.DefaultTile);
                        Vector2 pos = new Vector2(i, j);
                        Vector2 posOnScreen = Room.ConvertGridToScreenPosition(pos, false);
                        object[] args = new object[] { posOnScreen };
                        object grabObj = (Activator.CreateInstance(resolvedType, args));
                        Tiles.Add((ITile)grabObj);
                        //Debug.Print("Build: '{0}' placed on ({1}, {2}). (RoomBuilder)", resolvedType, pos.X, pos.Y);
                    }
                }
            }

            return new Room(player, Tiles, ItemsOnFloor, NPCs, roomData);
        }

        private static RoomData GetRoomData(int level, int id)
        {
            string path = PathHelper.GetRoomPath(level, id);

            using StreamReader r = new StreamReader(path);
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<RoomData>(json);
        }

        public static Room DevRoom(Game1 game, LinkPlayer player)
        {
            return RoomBuilder.Build(game, player, 1, -1);
        }
    }
}
        
