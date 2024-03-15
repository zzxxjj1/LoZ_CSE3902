using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    class GameOverState : IGameState
    {
        public Game1 game;

        private SpriteFont font;
        private string title = "GAME OVER";

        public GameOverState(Game1 game)
        {
            this.game = game;
            SoundManager.Instance.SetBGM(SoundEnum.BGM_GameOver);

            font = game.Content.Load<SpriteFont>("Fonts/Font_8px");
        }
        public void CommandSetUp()
        {
            foreach (IController controller in game.controllerList)
            {
                controller.SetCommand(GameStateEnum.GameOver);
            }
        }
        public void Update()
        {
            foreach (IController controller in game.controllerList)
            {
                controller.Update();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Black);
            GameUtility.Instance.HUDDrawingBegin();
            GameUtility.Instance.SpriteBatchHUD.DrawString(
                font, title, new Vector2(80, 50), Color.White);
            GameUtility.Instance.SpriteBatchHUD.DrawString(
                font, "Press R To Restart", new Vector2(80, 140), Color.White);
            GameUtility.Instance.SpriteBatchHUD.End();
        }

        public void Reset()
        {
            game.gameState = new TitleState(game);
        }
    }
}

