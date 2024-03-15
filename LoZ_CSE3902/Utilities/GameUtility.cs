using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class GameUtility
    {

        // nes resolution on a standard NTSC TV 
        public readonly int virturalWidth = GameAttributes.Window.NTSCResolutionWidth;
        public readonly int virturalHeight = GameAttributes.Window.NTSCResolutionHeight;
        public readonly int scalingFactor = 3;

        private readonly int gameWindowHeight = GameAttributes.Window.InGameHeight;
        private readonly int gameWindowWidth = GameAttributes.Window.InGameWidth;

        private readonly int hudWidth = GameAttributes.Window.HUDBarWidth;
        private readonly int hudHeight = GameAttributes.Window.HUDBarHeight;

        private static GameUtility instance = null;
        private static readonly object _lock = new object();

        public ContentManager ContentManager { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public SpriteBatch SpriteBatchHUD { get; private set; }

        static Texture2D _pointTexture;


        public GameUtility()
        {
        }
        public static GameUtility Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new GameUtility();
                    }

                    return instance;
                }
            }
        }

        public void SetContentManager(ContentManager contentManager)
        {
            this.ContentManager = contentManager;
        }

        public void SetSpriteBatch(SpriteBatch spriteBatch)
        {
            this.SpriteBatch = spriteBatch;
        }
        public void SetSpriteBatchHUD(SpriteBatch spriteBatch)
        {
            this.SpriteBatchHUD = spriteBatch;
        }

        public void GamePlayDrawingBegin()
        {
            SpriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointWrap, null, null, null, 
                Matrix.CreateTranslation(new Vector3(0, hudHeight, 0)) * Matrix.CreateScale(scalingFactor));
        }

        public void GamePlayDrawingBegin(Vector2 offset)
        {
            SpriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointWrap, null, null, null,
                Matrix.CreateTranslation(new Vector3(offset.X, hudHeight + offset.Y, 0))
                * Matrix.CreateScale(scalingFactor));
        }
        public void GamePlayDrawingBegin(bool isOffsetByHUDPosition)
        {
            if (isOffsetByHUDPosition) GamePlayDrawingBegin();
            else 
                SpriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointWrap, null, null, null,
                    Matrix.CreateTranslation(new Vector3(0, 0, 0)) * Matrix.CreateScale(scalingFactor));
        }

        public void HUDDrawingBegin()
        {
            SpriteBatchHUD.Begin(SpriteSortMode.Immediate, null, SamplerState.PointWrap, null, null, null,
                Matrix.CreateTranslation(new Vector3(0, 0, 0)) * Matrix.CreateScale(scalingFactor));
        }

        public void HUDDrawingBegin(Vector2 offset)
        {
            SpriteBatchHUD.Begin(SpriteSortMode.Immediate, null, SamplerState.PointWrap, null, null, null,
                Matrix.CreateTranslation(new Vector3(offset.X, offset.Y, 0))
                * Matrix.CreateScale(scalingFactor));
        }

        public void DrawBoarder(Rectangle rectangle, Color color, int lineWidth)
        {
            if (_pointTexture == null)
            {
                _pointTexture = new Texture2D(SpriteBatch.GraphicsDevice, 1, 1);
                _pointTexture.SetData<Color>(new Color[] { Color.White });
            }

            SpriteBatch.Draw(_pointTexture, new Rectangle(
                rectangle.X, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            SpriteBatch.Draw(_pointTexture, new Rectangle(
                rectangle.X, rectangle.Y, rectangle.Width + lineWidth, lineWidth), color);
            SpriteBatch.Draw(_pointTexture, new Rectangle(
                rectangle.X + rectangle.Width, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            SpriteBatch.Draw(_pointTexture, new Rectangle(
                rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + lineWidth, lineWidth), color);
        }
    }
}
