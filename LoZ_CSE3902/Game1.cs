using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace LoZ_CSE3902
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public SpriteBatch spriteBatchHUD;
        public List<IController> controllerList;

        public int virturalWidth = GameAttributes.Window.NTSCResolutionWidth;
        public int virturalHeight = GameAttributes.Window.NTSCResolutionHeight;
        public int scalingFactor = GameAttributes.Window.ScalingFactor;

        public IGameState gameState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            /* Set buffer here works in 3.7.1, but a bug cause 3.8 doesn't recognize it.
             * see https://github.com/MonoGame/MonoGame/pull/7299 */
            // graphics.PreferredBackBufferHeight = virturalHeight * scalingFactor;
            // graphics.PreferredBackBufferWidth = virturalWidth * scalingFactor;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            this.Window.Title = "TLoZ NES CSE3902 Group04";
            this.IsMouseVisible = true;

            //       A known bug about window size buffer
            //       need this line to set default window size correctly.
            //       https://github.com/MonoGame/MonoGame/pull/7299
            graphics.PreferredBackBufferHeight = virturalHeight * scalingFactor;
            graphics.PreferredBackBufferWidth = virturalWidth * scalingFactor;
            graphics.ApplyChanges();

            controllerList = new List<IController>()
            {
                new KeyboardController(this),
                new MouseController(this)
            };

            gameState = new TitleState(this);
            gameState.CommandSetUp();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatchHUD = new SpriteBatch(GraphicsDevice);
            GameUtility.Instance.SetSpriteBatch(spriteBatch);
            GameUtility.Instance.SetSpriteBatchHUD(spriteBatchHUD);
            GameUtility.Instance.SetContentManager(Content);

            LinkSpriteFactory.Instance.LoadAllTextures(Content);
            NPCSpriteFactory.Instance.LoadAllTextures(Content);
            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            TilesSpriteFactory.Instance.LoadAllTextures(Content);
            HUDSpriteFactory.Instance.LoadAllTextures(Content);

            SoundManager.Instance.LoadAllResources(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // I just want to be happy.
        }

        protected override void Update(GameTime gameTime)
        {
            gameState.Update();
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            
            gameState.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        public void Reset()
        {
            gameState = new TitleState(this);
            gameState.CommandSetUp();
        }
    }
}