using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{ 
    public interface INPC : ICollider
    {
        Vector2 Pos { get; set; }
        bool IsAlive { get; set; }

        void Draw(SpriteBatch spriteBatch);
        void Update();
        void TakeDamage(Direction side);
        void ChangeDirection(float length, Direction side);
        Vector2 GetPos();
        
    }
}

