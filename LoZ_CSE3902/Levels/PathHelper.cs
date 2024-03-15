using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace LoZ_CSE3902
{
	public static class PathHelper
    {
		private static readonly string CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		private static readonly string LevelFolder = @"Levels";
		private static readonly string FileFolder = @"RoomFiles";
		private static readonly string LevelDirectory = Path.Combine(CurrentDirectory, LevelFolder);
		private static readonly string FileDirectory = Path.Combine(LevelDirectory, FileFolder);
		private static readonly string MappingName = @"Mapping.json";

		public static string GetLevelPath(int level)
		{
			string LevelPath;
			switch (level)
            {
				case -1:
					LevelPath = @"Debug";
					break;
				case 0:
					LevelPath = @"Overworld";
					break;
				default:
					LevelPath = @"Level_" + Convert.ToString(level);
					break;
			}

			return Path.Combine(FileDirectory, LevelPath);
		}

		public static string GetMappingPath(int level)
        {
			return Path.Combine(@GetLevelPath(level), @MappingName);
        }

		public static string GetRoomPath(int level, int id)
        {
			string roomPath = @"Room_" + Convert.ToString(id) + @".json";
			return Path.Combine(GetLevelPath(level), roomPath);
		}
	}
}
