using System;
using System.Collections.Generic;
using System.Text;

namespace LoZ_CSE3902
{
    public class CollisionManager
    {
        private Game1 game;
        private CollisionDetector detector;


        public CollisionManager(Game1 game)
        {
            this.game = game;
        }

        public void Update()
        {
            detector.Update();
        }

        public void SetDetector(Room room, LinkPlayer player)
        {
            detector = new CollisionDetector(game, room, player);
        }

        public void DeleteCollider(ICollider collider)
        {
            detector.DeleteColliders(collider);
        }

        public void DeleteNPC(ICollider collider)
        {
            detector.DeleteNPC(collider);
        }

        public void AddCollider(ICollider collider)
        {
            detector.AddColliders(collider);
        }
    }
}
