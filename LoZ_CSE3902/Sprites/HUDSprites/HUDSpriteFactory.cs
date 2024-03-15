using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LoZ_CSE3902
{
    public class HUDSpriteFactory : ISpriteFactory
    {
		private Texture2D HUDTexture;
		private Texture2D SelectTexture;
		private Texture2D PlaceholderTexture;
		private Texture2D[,] MinimapTexture; // x for level, y for revealed or not
		private Texture2D[] BigMapTexture;
		private Texture2D TitleScreen;

		public Rectangle BarFrameSize { get; private set; }

		public Point TokenSize;
		public Rectangle NumberTextureSize { get; private set; }
		private Point NumberColRow, NumberOffset;
		public readonly List<char> CharList = new List<char> {'x', 
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a'};

		public Rectangle HeartTextureSize { get; private set; }
		private Point HeartColRow, HeartOffset;

		public Rectangle InventoryMenuTextureSize { get; private set; }
		public Rectangle InventoryMapTextureSize { get; private set; }

		public Rectangle SelectTextureSizeRed { get; private set; }
		public Rectangle SelectTextureSizeBlue { get; private set; }
		private const int SelectTextureFrameCount = 2;

		public Rectangle[] MinimapTextureSize { get; private set; }
		public Rectangle BigmapTextureSize { get; private set; }

		public Rectangle TitleScreenSize { get; private set; }
		public Rectangle SplashSize1 { get; private set; }
		public Rectangle SplashSize2 { get; private set; }
		public Rectangle WaveSize1 { get; private set; }
		public Rectangle WaveSize2 { get; private set; }
		public Rectangle WaveSize3 { get; private set; }

		private static HUDSpriteFactory instance = new HUDSpriteFactory();
		public static HUDSpriteFactory Instance
		{
			get
			{
				return instance;
			}
		}

		private HUDSpriteFactory()
		{
		}

		public void LoadAllTextures(ContentManager content)
		{
			HUDTexture = content.Load<Texture2D>("HUD/HUD");
			SelectTexture = content.Load<Texture2D>("HUD/Select");
			PlaceholderTexture = content.Load<Texture2D>("HUD/Placeholder");
			MinimapTexture = new Texture2D[2, 2];
			MinimapTexture[0, 0] = content.Load<Texture2D>("HUD/OverworldMap");
			MinimapTexture[0, 1] = content.Load<Texture2D>("HUD/OverworldMap");
			MinimapTexture[1, 0] = content.Load<Texture2D>("HUD/Dungeon1_nomap");
			MinimapTexture[1, 1] = content.Load<Texture2D>("HUD/Dungeon1_minimap");
			BigMapTexture = new Texture2D[2];
			BigMapTexture[1] = content.Load<Texture2D>("HUD/Dungeon1_bigmap");
			TitleScreen = content.Load<Texture2D>("HUD/TitleScreen");

			BarFrameSize = new Rectangle(258, 11, 
				GameAttributes.Window.HUDBarWidth, 
				GameAttributes.Window.HUDBarHeight);

			TokenSize = new Point(GameAttributes.Window.TokenWidth,
				GameAttributes.Window.TokenHeight);
			NumberTextureSize = new Rectangle(519, 117, 108, 8);
			NumberColRow = new Point(12, 1);
			NumberOffset = new Point(1, 0);

			HeartTextureSize = new Rectangle(627, 117, 3 * 8 + 3, 8);
			HeartColRow = new Point(3, 1);
			HeartOffset = new Point(1, 0);

			InventoryMenuTextureSize = new Rectangle(1, 11, 256, 88);
			InventoryMapTextureSize = new Rectangle(258, 112, 256, 88);

			SelectTextureSizeRed = new Rectangle(0, 0, 16, 16);
			SelectTextureSizeBlue = new Rectangle(16, 0, 16, 16);

			MinimapTextureSize = new Rectangle[2];
			MinimapTextureSize[0] = new Rectangle(0, 0, 64, 40);
			MinimapTextureSize[1] = new Rectangle(0, 0, 47, 23);

			BigmapTextureSize = new Rectangle(0, 0, 64, 64);

			TitleScreenSize = new Rectangle(1, 11, 
				GameAttributes.Window.HUDBarWidth, GameAttributes.Window.HUDBarHeight);
			SplashSize1 = new Rectangle(846, 11, 32, 16);
			SplashSize2 = new Rectangle(879, 11, 32, 16);
			WaveSize1 = new Rectangle(776, 28, 32, 16);
			WaveSize2 = new Rectangle(809, 28, 32, 16);
			WaveSize3 = new Rectangle(842, 28, 32, 16);
		}

		public ISprite CreateHUDBarSprite()
		{
			return new HUDStaticSprite(HUDTexture, BarFrameSize);
		}

		public NumberSprite CreateNumberSprite()
		{
			return new NumberSprite(CharList, HUDTexture, NumberTextureSize, TokenSize, 
				NumberColRow, NumberOffset);
		}

		public HeartSprite CreateHeartSprite()
        {
			return new HeartSprite(HUDTexture, HeartTextureSize, TokenSize, HeartOffset);
        }

		public ISprite CreateInventoryMenuSprite()
		{
			return new HUDStaticSprite(HUDTexture, InventoryMenuTextureSize);
		}
		public ISprite CreateInventoryMapSprite()
		{
			return new HUDStaticSprite(HUDTexture, InventoryMapTextureSize);
		}
		public ISprite CreateSelectSprite()
		{
			return new HUDAnimatingSprite(SelectTexture, SelectTextureSizeRed, SelectTextureFrameCount);
		}
		public ISprite CreateColorBlockSprite(Point size, Color color)
		{
			return new ColorBlockSprite(PlaceholderTexture, size, color);
		}
		public MiniMapSprite CreateMinimapSprite(int index)
		{
			return new MiniMapSprite(
				MinimapTexture[index, 0], MinimapTexture[index, 1], MinimapTextureSize[index], index);
		}
		public ISprite CreateBigmapSprite(int index)
		{
			return new HUDStaticSprite(BigMapTexture[index], BigmapTextureSize);
		}
		public ISprite CreateTitleScreen()
        {
			return new HUDStaticSprite(TitleScreen, TitleScreenSize);
        }
	}
}
