using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LoZ_CSE3902
{
    public class RoomData
    {
        public int ID { get; set; }
        public int Level { get; set; }
        public bool InUnderworld { get; set; }
        // public bool WallDestructable { get; set; }
        public string DefaultTile { get; set; }
        public Dictionary<Direction, DoorState> DoorData { get; set; }

        // Vector2 array for object position in a 16x11 frame
        // if this is in the dungeon(underworld), the frame will be 12x7
        public Dictionary<string, Point[]> ObjectDictionary { get; set; }

        public RoomData()
        {

        }

        public static RoomData Sample()
        {
            RoomData roomData = new RoomData();
            roomData.ID = 12;
            roomData.Level = 1;
            roomData.InUnderworld = true;
            //roomData.WallDestructable = false;
            roomData.DefaultTile = "Floor";

            roomData.DoorData = new Dictionary<Direction, DoorState>();
            roomData.DoorData.Add(Direction.Left, DoorState.Wall);
            roomData.DoorData.Add(Direction.Right, DoorState.Closed);
            roomData.DoorData.Add(Direction.Up, DoorState.Locked);
            roomData.DoorData.Add(Direction.Down, DoorState.Open);

            roomData.ObjectDictionary = new Dictionary<string, Point[]>();
            roomData.ObjectDictionary.Add("Block", new Point[]
                {new Point(1,1), new Point(10, 1),
                new Point(1, 5), new Point(10, 5)});
            roomData.ObjectDictionary.Add("Water", new Point[]
                {new Point(4, 1), new Point(4, 2),
                new Point(4, 3), new Point(4, 4)});
            roomData.ObjectDictionary.Add("Gel", new Point[] { new Point(7, 2) });
            roomData.ObjectDictionary.Add("Key", new Point[] { new Point(7, 3) });
            roomData.ObjectDictionary.Add("Heart", new Point[] { new Point(7, 4) });

            string json = JsonConvert.SerializeObject(roomData, Formatting.Indented);
            StreamWriter file = new StreamWriter(@".\Room_Sample.json");
            file.WriteLine(json);
            file.Close();

            return roomData;
        }
    }

}
