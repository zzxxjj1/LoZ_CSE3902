using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace LoZ_CSE3902
{
    public class LevelMapping
    {
        public static readonly Point InvalidRoomPos = new Point(-1, -1);

        public int Level { get; set; }
        public string Name { get; set; }

        // Json.NET requires fields to be public
        public Point Size { get; set; } // X for row, Y for column 
        public int[,] Layout { get; set; }
        public int StartRoom { get; set; }
        public int CurrentRoom { get; set; }

        public Dictionary<int, Room> LoadedRooms { get; set; } 
            = new Dictionary<int, Room>();


        public static LevelMapping Create(int level)
        {
            string path = PathHelper.GetMappingPath(level);

            using StreamReader r = new StreamReader(path);
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<LevelMapping>(json);
        }
        public void InitializeAllRooms(Game1 game, LinkPlayer player)
        {
            foreach (int id in Layout)
            {
                if (id != 0)
                {
                    Room createdRoom = RoomBuilder.Build(game, player, Level, id);
                    LoadedRooms.Add(id, createdRoom);
                }
            }
            LoadedRooms.Add(-1, RoomBuilder.Build(game, player, Level, -1));
        }

        public Point GetLayoutPositionFromID(int currentID)
        {
            for (int row = 0; row < Size.X; row++)
            {
                for (int col = 0; col < Size.Y; col++)
                {
                    if (Layout[row, col] == currentID)
                    {
                        return new Point(row, col);
                    }
                }
            }
            return InvalidRoomPos;
        }

        public int GetNextRoomID(int currentID, Direction direction)
        {
            if (currentID == -1) return StartRoom;

            Point pos = GetLayoutPositionFromID(currentID);
            int nextRoomID = 0;
            bool retrieved = true;
            switch (direction)
            {
                case Direction.Up:
                    if (pos.X > 0) pos.X--; else retrieved = false;
                    break;
                case Direction.Left:
                    if (pos.Y > 0) pos.Y--; else retrieved = false;
                    break;
                case Direction.Right:
                    if (pos.Y < Size.Y - 1) pos.Y++; else retrieved = false;
                    break;
                case Direction.Down:
                    if (pos.X < Size.X - 1) pos.X++; else retrieved = false;
                    break;
            }

            if (!retrieved)
            {
                Debug.Print("GetNextRoomID: Cannot locate adjacent room.. Return 0");
            }
            else
            {
                nextRoomID = Layout[pos.X, pos.Y];
            }

            return nextRoomID;
        }
        public Room GetNextRoom(Direction direction)
        {
            int id = GetNextRoomID(CurrentRoom, direction);
            if (id == 0)  id = -1; // if invalid room, go to dev room
            return LoadedRooms.GetValueOrDefault(id);
        }
        public Room GetCurrentRoom()
        {
            if (CurrentRoom == 0)
            {
                CurrentRoom = -1;
            }
            return LoadedRooms.GetValueOrDefault(CurrentRoom);
        }
        public void SwitchRoomBySide(Direction side)
        {
            CurrentRoom = GetNextRoomID(CurrentRoom, side);
        }
        public void SaveRoom(Room room)
        {
            LoadedRooms.Remove(room.data.ID);
            LoadedRooms.Add(room.data.ID, room);
        }


        public static LevelMapping Sample()
        {
            LevelMapping data = new LevelMapping();
            data.Level = 1;
            data.Name = "The Eagle";
            data.Size = new Point(6, 6);
            data.Layout = new int[6, 6]{
                {   0,   15,  16,  0,  0,  0},
                {   0,   13,  14,  0,  17,  18},
                {   8,   9,  10,  11,  12,  0},
                {   0,   5,  6,  7,  0,  0},
                {   0,   0,  4,  0,  0,  0},
                {   0,   2,  1,  3,  0,  0}
            };
            data.StartRoom = 1;



            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            StreamWriter file = new StreamWriter(@".\Mapping_Sample.json");
            file.WriteLine(json);
            file.Close();

            return data;
        }

    }
}
