using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoZ_CSE3902
{
    public class BigMapDisplay
    {
        private GamePlayState gamePlay;
        private LinkPlayer player;
        private ISprite background, mapSprite, compassSprite, bigmap;
        private Vector2 pos;

        private readonly Vector2 mapPosition = new Vector2(48, 24);
        private readonly Vector2 compassPosition = new Vector2(44, 64);
        private readonly Vector2 bigmapPosition = new Vector2(128, 8);


        public BigMapDisplay(GamePlayState state)
        {
            pos = new Vector2(0);
            player = state.player;
            gamePlay = state;

            background = HUDSpriteFactory.Instance.CreateInventoryMapSprite();

            if (GamePlayState.hudBar.HasMap)
                mapSprite = ItemSpriteFactory.Instance.CreateMapSprite();
            else
            {
                Point size = ItemSpriteFactory.Instance.MapFrameSize.Size;
                mapSprite = HUDSpriteFactory.Instance.CreateColorBlockSprite(size, Color.Black);
            }

            if (GamePlayState.hudBar.HasCompass)
                compassSprite = ItemSpriteFactory.Instance.CreateCompassSprite();
            else
            {
                Point size = new Point(16);
                compassSprite = HUDSpriteFactory.Instance.CreateColorBlockSprite(size, Color.Black);
            }

            bigmap = HUDSpriteFactory.Instance.CreateBigmapSprite(state.mapping.Level);
        }

        public void Update()
        {
            background.Update();
        }

        public void Draw(Vector2 destPos)
        {
            background.Draw(destPos);
            mapSprite.Draw(destPos + mapPosition);
            compassSprite.Draw(destPos + compassPosition);
            bigmap.Draw(destPos + bigmapPosition);
        }
        public void UpdateMapDisplay(LinkPlayer player)
        {


        }

    }
}
