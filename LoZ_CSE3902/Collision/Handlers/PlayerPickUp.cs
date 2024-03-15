using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class PlayerPickUp : ICommand
    {
        private LinkPlayer link;
        private IItem item;

        public PlayerPickUp(LinkPlayer link, IItem item)
        {
            this.link = link;
            this.item = item;
        }
        public PlayerPickUp(ICollider player, ICollider item, Direction side)
        {
            this.link = (LinkPlayer)player;
            this.item = (IItem)item;
        }


        public void Execute()
        {
            item.Pickup();
        }
    }
}
