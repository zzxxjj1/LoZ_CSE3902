using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public interface ILinkState
    {
        void Update();
        void Draw(SpriteBatch spriteBatch);
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void UseItemA();
        void UseItemB();
        void PickUp();
        void TakeDamage();
        void SetInvincibleFrames(int frames);
    }
}
