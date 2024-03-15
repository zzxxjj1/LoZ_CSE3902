using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class ClonePickUp : ICommand
    {
        private LinkClone link;
        private IItem item;

        public ClonePickUp(ICollider player, ICollider item, Direction side)
        {
            this.link = (LinkClone)player;
            this.item = (IItem)item;
        }


        public void Execute()
        {
            item.Pickup();
        }
    }
}
