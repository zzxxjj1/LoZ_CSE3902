using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class NPCBlockingMovement : ICommand
    {
        private INPC npc;
        private Direction side;
        private float penetration_length;

        public NPCBlockingMovement(INPC npc, Direction side, float length)
        {
            this.npc = npc;
            this.side = side;
            this.penetration_length = length;
        }
        public NPCBlockingMovement(ICollider npc, ICollider tile, Direction side)
        {
            this.npc = (INPC)npc;
            this.side = side;
            penetration_length = CollisionHandler.GetPenetrationLength(npc, tile, side);
        }

        public void Execute()
        {
            npc.ChangeDirection(penetration_length, side);
        }
    }
}
