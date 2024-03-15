using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoZ_CSE3902
{
    public class InventoryDisplay
    {
        private GamePlayState gamePlay;
        private LinkPlayer player;
        private ISprite inventorySprite, selectSprite;
        
        private List<ISprite> itemBackgrounds;
        private List<GadgetForLink> availableGadgetList;
        private Dictionary<GadgetForLink, ISprite> display;

        public Point[] gadgetDestination;
        private readonly Point GadgetTokenPosition = new Point(128, 48);
        private readonly Point TokenSize = new Point(16, 16);
        private readonly Point ColRow = new Point(4, 2);
        private readonly Point Offset = new Point(8, 0);

        private readonly Vector2 ItemBPosition = new Vector2(68, 48);

        private readonly Vector2 BombPosition = new Vector2(156, 48);
        private readonly Vector2 ArrowPosition = new Vector2(176, 48);
        private readonly Vector2 BowPosition = new Vector2(184, 48);

        private Vector2 SelectStartPosition = new Vector2(128, 48);
        private Vector2[] SelectPosition = new Vector2[8];
        private Point SelectSpriteSize = HUDSpriteFactory.Instance.SelectTextureSizeBlue.Size;
        private const int GadgetRowOffset = 0; // px
        private const int GadgetColumnOffset = 8; //px
        private const int SelectBlickFrameCount = 12;
        private int nextFrameCount = SelectBlickFrameCount;
        

        public InventoryDisplay(GamePlayState state)
        {
            player = state.player;
            gamePlay = state;

            inventorySprite = HUDSpriteFactory.Instance.CreateInventoryMenuSprite();
            selectSprite = HUDSpriteFactory.Instance.CreateSelectSprite();
            itemBackgrounds = new List<ISprite>();
            availableGadgetList = new List<GadgetForLink>();

            gadgetDestination = new Point[8];
            for (int Row = 0; Row < ColRow.Y; Row++)
            {
                for (int Col = 0; Col < ColRow.X; Col++)
                {
                    int x = GadgetTokenPosition.X + Col * (TokenSize.X + Offset.X);
                    int y = GadgetTokenPosition.Y + Row * (TokenSize.Y + Offset.Y);
                    gadgetDestination[Row * ColRow.X + Col] = new Point(x, y);

                    float xf = SelectStartPosition.X + Col * (SelectSpriteSize.X + GadgetColumnOffset);
                    float yf = SelectStartPosition.Y + Row * (SelectSpriteSize.Y + GadgetRowOffset);
                    SelectPosition[Row * ColRow.X + Col] = new Vector2(xf, yf);
                }
            }

            itemBackgrounds = new List<ISprite>();
            for (int i = 0; i < ColRow.X * ColRow.Y; i++)
            {
                itemBackgrounds.Add(
                    HUDSpriteFactory.Instance.CreateColorBlockSprite(TokenSize, Color.Black));
            }

            display = new Dictionary<GadgetForLink, ISprite>();
            display.Add(GadgetForLink.Bomb, ItemSpriteFactory.Instance.CreateBombSprite());
            display.Add(GadgetForLink.Arrow, ItemSpriteFactory.Instance.CreateArrowSprite());
            display.Add(GadgetForLink.Bow, ItemSpriteFactory.Instance.CreateBowSprite());

        }

        public void Update()
        {
            inventorySprite.Update();
        }

        public void Draw(Vector2 destPos)
        {
            inventorySprite.Draw(destPos);
            SpriteBatch sb = GameUtility.Instance.SpriteBatchHUD;

            for(int i = 0; i < itemBackgrounds.Count; i++)
            {
                itemBackgrounds[i].Draw(gadgetDestination[i].ToVector2() + destPos);
            }

            //add gadget token here
            if (availableGadgetList.Contains(GadgetForLink.Bomb))
            {
                display.GetValueOrDefault(GadgetForLink.Bomb).Draw(sb, BombPosition + destPos);
            }
            if (availableGadgetList.Contains(GadgetForLink.Arrow))
            {
                display.GetValueOrDefault(GadgetForLink.Arrow).Draw(sb, ArrowPosition + destPos);
            }
            if (availableGadgetList.Contains(GadgetForLink.Bow))
            {
                display.GetValueOrDefault(GadgetForLink.Bow).Draw(sb, BowPosition + destPos);
            }
            if (availableGadgetList.Contains(GadgetForLink.Bow_Arrow))
            {
                display.GetValueOrDefault(GadgetForLink.Arrow).Draw(sb, ArrowPosition + destPos);
                display.GetValueOrDefault(GadgetForLink.Bow).Draw(sb, BowPosition + destPos);
            }

            nextFrameCount--;
            bool goNextFrame = nextFrameCount < 0;
            if (goNextFrame) nextFrameCount = SelectBlickFrameCount;
            switch (player.itemInUse)
            {
                case GadgetForLink.Bomb:
                    selectSprite.Draw(sb, SelectPosition[1] + destPos, goNextFrame);
                    display.GetValueOrDefault(GadgetForLink.Bomb).Draw(sb, ItemBPosition + destPos);
                    break;
                case GadgetForLink.Bow_Arrow:
                    selectSprite.Draw(sb, SelectPosition[2] + destPos, goNextFrame);
                    display.GetValueOrDefault(GadgetForLink.Arrow).Draw(sb, ItemBPosition + destPos);
                    break;
                case GadgetForLink.Boomerang:
                    selectSprite.Draw(sb, SelectPosition[0] + destPos, goNextFrame);
                    break;
                default:
                    Debug.Print(
                        "Draw: could not find gadget. (InventoryDisplay)");
                    break;
            }

            
        }
        public void UpdateListDisplay(LinkPlayer player)
        {
            availableGadgetList = new List<GadgetForLink>();
            foreach (GadgetForLink gadget in player.inventory.GadgetAvailableSet)
            {
                availableGadgetList.Add(gadget);
            }
        }

    }
}
