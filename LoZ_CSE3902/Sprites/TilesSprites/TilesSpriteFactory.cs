using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
    public class TilesSpriteFactory: ISpriteFactory
    {
        private Texture2D TileTexture;
        public List<Rectangle> TileFrameSize;

        private Texture2D ExteriorTexture;
        public Rectangle ExteriorFrameSize;
        public Rectangle[,] DoorFrameSize;
        // DoorFrameSize[Direction, State]


        private static TilesSpriteFactory instance = new TilesSpriteFactory();

        public static TilesSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private TilesSpriteFactory() { }

        public void LoadAllTextures(ContentManager content)
        {
            TileTexture = content.Load<Texture2D>("Obstacle/tiles");
            TileFrameSize = new List<Rectangle>();
            TileFrameSize.Add(new Rectangle(0, 0, 16, 16));
            for (int i = 0; i < 9; i++)
            {
                TileFrameSize.Add(new Rectangle(i * 16, 0, 16, 16));
            }

            ExteriorTexture = content.Load<Texture2D>("Obstacle/Exterior");
            ExteriorFrameSize = new Rectangle(3, 11, 256, 176);
            DoorFrameSize = new Rectangle[4,5];
            // DoorFrameSize[Direction, State]
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    DoorFrameSize[i, j] = new Rectangle(297 + 33 * j, 11 + 33 * i, 32, 32);
                }
            }

        }

        public ISprite CreateDoorSprite(Direction direction, DoorState doorState)
        {
            if (doorState.Equals(DoorState.Destructable)) 
                doorState = DoorState.Wall;
            return new TilesIdleSprite(ExteriorTexture, DoorFrameSize[(int)direction, (int)doorState]);
        }

        public ISprite CreateExteriorSprite()
        {
            return new TilesIdleSprite(ExteriorTexture, ExteriorFrameSize);
        }


        public ISprite CreateFloor()
        {
            return new TilesIdleSprite(TileTexture, TileFrameSize[1]);
        }

        public ISprite CreateBlock()
        {
            return new TilesIdleSprite(TileTexture, TileFrameSize[2]);
        }

        public ISprite CreateDragonBlockLeft()
        {
            return new TilesIdleSprite(TileTexture, TileFrameSize[3]);
        }

        public ISprite CreateDragonBlockRight()
        {
            return new TilesIdleSprite(TileTexture, TileFrameSize[4]);
        }

        public ISprite CreateSand()
        {
            return new TilesIdleSprite(TileTexture, TileFrameSize[5]);
        }

        public ISprite CreateWater()
        {
            return new TilesIdleSprite(TileTexture, TileFrameSize[6]);
        }

        public ISprite CreateRockStairs()
        {
            return new TilesIdleSprite(TileTexture, TileFrameSize[7]);
        }

        public ISprite CreateBlackSpace()
        {
            return new TilesIdleSprite(TileTexture, TileFrameSize[8]);
        }
        
        public ISprite CreateBrickWall()
        {
            return new TilesIdleSprite(TileTexture, TileFrameSize[9]);
        }

    }
}
