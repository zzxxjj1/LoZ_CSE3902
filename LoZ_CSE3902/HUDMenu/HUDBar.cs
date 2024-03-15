using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoZ_CSE3902
{
    public class HUDBar
    {
        private LinkPlayer player;
        private ISprite background, itemA, itemB;
        private NumberSprite rupee, bomb, key;
        private HeartSprite heart;
        private MiniMapSprite minimap;

        private Vector2 pos;
        private int dungeonIndex;

        public bool HasMap { get; set; }
        public bool HasCompass { get; set; }
        private GadgetForLink itemInUse;

        public HUDBar(LinkPlayer player)
        {
            pos = new Vector2(0);
            this.player = player;

            itemInUse = GadgetForLink.Null;

            background = HUDSpriteFactory.Instance.CreateHUDBarSprite();
            rupee = HUDSpriteFactory.Instance.CreateNumberSprite();
            bomb = HUDSpriteFactory.Instance.CreateNumberSprite();
            key = HUDSpriteFactory.Instance.CreateNumberSprite();
            heart = HUDSpriteFactory.Instance.CreateHeartSprite();

            itemA = ItemSpriteFactory.Instance.CreateSwordSprite();
            itemB = HUDSpriteFactory.Instance.CreateColorBlockSprite(GameAttributes.Window.ItemTokenSize, Color.Black);
            SetMiniMap(0);
        }

        public void Update()
        {
            rupee.SetCount(player.inventory.rupeeCount);
            bomb.SetCount(player.inventory.bombCount);
            key.SetCount(player.inventory.keyCount);
            heart.SetHeart(player.health, player.maxHealth);

            CheckItemBSprite();
        }

        public void Draw()
        {
            Draw(new Vector2(0));
        }
        public void Draw(Vector2 offset)
        {
            background.Draw(pos + offset);

            rupee.Draw(GameAttributes.Window.Pos.RupeeCount + offset);
            bomb.Draw(GameAttributes.Window.Pos.BombCount + offset);
            key.Draw(GameAttributes.Window.Pos.KeyCount + offset);
            heart.Draw(GameAttributes.Window.Pos.HeartDisplay + offset);

            itemA.Draw(GameAttributes.Window.Pos.ItemA + offset);
            itemB.Draw(GameAttributes.Window.Pos.ItemB + offset);

            minimap.Draw(GameAttributes.Window.Pos.MiniMapDisplay + offset);

        }

        public void SetMiniMap(int dungeonIndex)
        {
            this.dungeonIndex = dungeonIndex;
            minimap = HUDSpriteFactory.Instance.CreateMinimapSprite(dungeonIndex);
        }
        public void RevealMap(bool flag)
        {
            minimap.Reveal(flag);
            HasMap = flag;
        }
        public void ShowDestinationOnMap(bool flag)
        {
            minimap.ShowFinalRoom(flag);
            HasCompass = flag;
        }
        public void SetCurrentRoom(Point layout, Point posInLayout)
        {
            minimap.SetIndicatorPosition(layout, posInLayout);
        }
        public void CheckItemBSprite()
        {
            if (player.itemInUse != itemInUse)
            {
                itemInUse = player.itemInUse;
                switch (itemInUse)
                {
                    case GadgetForLink.Bomb:
                        itemB = ItemSpriteFactory.Instance.CreateBombSprite();
                        break;
                    case GadgetForLink.Bow_Arrow:
                        itemB = ItemSpriteFactory.Instance.CreateArrowSprite();
                        break;
                    case GadgetForLink.Boomerang:
                        itemB = ItemSpriteFactory.Instance.CreateBoomerangSprite();
                        break;
                    default:
                        Debug.Print(
                            "UseGadget: could not find gadget. (InventoryHelper)");
                        break;
                }
            }
        }
    }
}
