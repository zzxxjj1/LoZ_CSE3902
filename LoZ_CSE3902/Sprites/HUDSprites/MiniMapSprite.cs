using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace LoZ_CSE3902
{
    public class MiniMapSprite : ISprite
    {
        public Texture2D fullMap, mapWithOnlyFinalRoom, currentTexture;
        public Rectangle textureSize;
        private readonly Point MapAreaSize = new Point(64, 40);
        private readonly Point IndicatorSize = new Point(3, 3);
        private ISprite background, playerIndicator;
        private Vector2 indicatorPosition; 

        private int levelIndex;
        private string levelLable;
        private SpriteFont font;
        
        private bool isFinalRoomShown = false;
        private bool isMapRevealed = false;

        public MiniMapSprite(Texture2D blackTexture, Texture2D fullTexture, Rectangle textureSize, int levelIndex)
        {
            fullMap = fullTexture;
            mapWithOnlyFinalRoom = blackTexture;
            this.textureSize = textureSize;
            this.levelIndex = levelIndex;

            font = GameUtility.Instance.ContentManager.Load<SpriteFont>("Fonts/Font_8px");
            levelLable = "LEVEL-" + levelIndex;
            Reveal(false);
            ShowFinalRoom(false);

            background = HUDSpriteFactory.Instance.CreateColorBlockSprite(MapAreaSize, Color.Black);
            playerIndicator = HUDSpriteFactory.Instance.CreateColorBlockSprite(IndicatorSize, Color.Yellow);

        }

        public void Update() { }
        public void Reveal(bool flag) 
        {
            isMapRevealed = flag;
            if (flag) currentTexture = fullMap;
            else currentTexture = mapWithOnlyFinalRoom;
        }
        public void ShowFinalRoom(bool flag)
        {
            isFinalRoomShown = flag;
            if (flag) currentTexture = mapWithOnlyFinalRoom;
            if (isMapRevealed) currentTexture = fullMap;
        }
        private Vector2 SetCenteredDrawPosition(Vector2 pos)
        {
            pos.X += (MapAreaSize.X - textureSize.Width) / 2;
            pos.Y += MapAreaSize.Y - GameAttributes.Window.TokenHeight - textureSize.Height;
            return pos;
        }
        public void SetIndicatorPosition(Point layout, Point posInLayout)
        {
            Vector2 indicatorOffset = new Vector2(-6, -0.5f);

            indicatorPosition = textureSize.Size.ToVector2();
            float tokenWidth, tokenHeight; // per one room
            tokenWidth = (float)textureSize.Width / layout.Y;
            tokenHeight = (float)textureSize.Height / layout.X;

            indicatorPosition.X = tokenWidth * (posInLayout.Y);
            indicatorPosition.Y -= tokenHeight * (layout.X - posInLayout.X);
            indicatorPosition += indicatorOffset;
        }

        public void Draw(Vector2 destPos)
        {
            background.Draw(destPos);

            Vector2 centeredPos = SetCenteredDrawPosition(destPos);
            Rectangle centeredDest = new Rectangle(centeredPos.ToPoint(), textureSize.Size);

            if (levelIndex == 0)
            {
                GameUtility.Instance.SpriteBatchHUD.Draw(
                    currentTexture, centeredDest, textureSize, Color.White);
            }
            else {
                Rectangle labelOffset = new Rectangle(centeredDest.X, centeredDest.Y + 8,
                    centeredDest.Width, centeredDest.Height);
                GameUtility.Instance.SpriteBatchHUD.DrawString(font, levelLable,
                    destPos, Color.White);
                if (isMapRevealed || isFinalRoomShown)
                    GameUtility.Instance.SpriteBatchHUD.Draw(
                        currentTexture, labelOffset, textureSize, Color.White);
            }

            playerIndicator.Draw(
                SetCenteredDrawPosition(destPos)
                + SetCenteredDrawPosition(indicatorPosition));
        }
        public void Draw(Vector2 destinationVector, bool goNextFrame)
        {
            Draw(destinationVector);
        }
        public void Draw(SpriteBatch spritebatch, Vector2 destinationVector)
        {
            Draw(destinationVector);
        }
        public void Draw(SpriteBatch spritebatch, Vector2 destinationVector, bool goNextFrame)
        {
            Draw(destinationVector, goNextFrame);
        }
    }
}
