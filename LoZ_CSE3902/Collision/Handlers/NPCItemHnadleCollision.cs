using System;
namespace LoZ_CSE3902
{
    public class NPCItemHnadleCollision : ICommand
    {
        private INPC npc;
        private IItem item;
        private Direction side;

        public NPCItemHnadleCollision(INPC npc, IItem item, Direction side)
        {
            this.npc = npc;
            this.item = item;
            this.side = side;
        }

        public void Execute()
        {
            npc.TakeDamage(side);
        }
    }
}
