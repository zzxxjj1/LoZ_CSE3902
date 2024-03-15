using System;
namespace LoZ_CSE3902
{
    public class LinkDamagedByItemHandleCollision : ICommand
    {

        private LinkPlayer link;
        private IItem item;


        public LinkDamagedByItemHandleCollision(LinkPlayer link, IItem item)
        {
            this.link = link;
            this.item = item;
        }

        public void Execute()
        {
            link.TakeDamage();
        }
    }
}
