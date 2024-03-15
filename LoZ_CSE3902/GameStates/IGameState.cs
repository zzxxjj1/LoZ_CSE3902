using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public interface IGameState
{
    void Update();
    void Draw(SpriteBatch spriteBatch);
    void CommandSetUp();
}
