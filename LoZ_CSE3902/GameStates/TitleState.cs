using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    class TitleState : IGameState
    {
        public Game1 game;

        private SpriteFont font;
        private bool bgmStarted = false;
        //private ISprite background;
        private Texture2D background;
        

        public TitleState(Game1 game)
        {
            this.game = game;
            bgmStarted = false;

            font = game.Content.Load<SpriteFont>("Fonts/Font_8px");
            background = game.Content.Load<Texture2D>("HUD/TitleScreen1");
        }
        public void CommandSetUp()
        {
            foreach (IController controller in game.controllerList)
            {
                controller.SetCommand(GameStateEnum.TitleScreen);
            }
        }
        public void Update()
        {
            foreach (IController controller in game.controllerList)
            {
                controller.Update();
            }
            if (!bgmStarted) {
                bgmStarted = true;
                SoundManager.Instance.SetBGM(SoundEnum.BGM_Title); 
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            GameUtility.Instance.HUDDrawingBegin();
            GameUtility.Instance.SpriteBatchHUD.Draw(background, new Vector2(0, 0),
                new Rectangle(0, 0,
                256 * GameAttributes.Window.ScalingFactor,
                232 * GameAttributes.Window.ScalingFactor),
                Color.White);
            //GameUtility.Instance.SpriteBatchHUD.DrawString(
            //    font, title, new Vector2(30, 20), Color.Black);
            GameUtility.Instance.SpriteBatchHUD.DrawString(
                font, "Press Space To Start", new Vector2(50, 150), Color.Black);
            GameUtility.Instance.SpriteBatchHUD.End();

           
        }

        public void Reset()
        {
            game.gameState = new TitleState(game);
        }
    }
}

