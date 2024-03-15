using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public interface IItem : ICollider
    {
        void Update();
        void Pickup();
        void Draw(SpriteBatch spriteBatch);
        bool IsPicked();
    }
}

