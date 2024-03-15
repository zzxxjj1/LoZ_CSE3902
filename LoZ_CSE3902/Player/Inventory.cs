using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoZ_CSE3902
{
    public class Inventory
    {
        private LinkPlayer player;
        public HashSet<GadgetForLink> GadgetAvailableSet;

        public int rupeeCount, keyCount, bombCount;

        // on-screen gadget entity
        private static IGadgetEffect GadgetEffect;
        private SwordHead swordRectangle;
        //private int terminateFrameLeft;
        //private bool isTerminating;


        public Inventory(LinkPlayer player)
        {
            this.player = player;
            GadgetAvailableSet = new HashSet<GadgetForLink>();
            rupeeCount = 0;
            keyCount = 0;
            bombCount = 0;
        }
        public void Update()
        {
            if (GadgetEffect != null)
            {
                GadgetEffect.Update();
            }

            //CheckBagCapacity();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (GadgetEffect != null)
            {
                GadgetEffect.Draw(spriteBatch);
            }

            if (HitBox.showBoarder && player.state is LinkSwingSwordState)
            {
                swordRectangle.Draw(spriteBatch);
            }
        }
        public void PickUp(ItemInGame item)
        {
            throw new NotImplementedException(
                "Inventory: please let IItem to handle pickup action.");
        }

        public void CheckBagCapacity()
        {
            if (rupeeCount > GameAttributes.Player.MaxRupee)
                rupeeCount = GameAttributes.Player.MaxRupee;
            if (keyCount > GameAttributes.Player.MaxKey)
                keyCount = GameAttributes.Player.MaxKey;
            if (bombCount > GameAttributes.Player.MaxBomb)
                bombCount = GameAttributes.Player.MaxBomb;
            GadgetAvailableSet.Add(GadgetForLink.Bomb);
        }

        public bool UseKey()
        {
            bool isUsed = keyCount > 0;
            if (isUsed) keyCount--;
            return isUsed;
        }

        public Boolean UseGadget()
        {
            Boolean isUsed = false;
            switch (player.itemInUse)
            {
                // case GadgetEnum.Boomerang: UseBoomerang(); break;
                case GadgetForLink.Bomb: isUsed = PlaceBomb(); break;
                case GadgetForLink.Bow_Arrow: isUsed = ShootArrow(); break;

                default: 
                    Debug.Print(
                        "UseGadget: could not find gadget. (InventoryHelper)");
                    break;
            }
            return isUsed;
        }

        public void TerminateGadget()
        {
            GadgetEffect = null;
        }

        public void TryShootSword()
        {
            if (player.health == player.maxHealth)
            {
                GadgetEffect = new SwordShooted(player);
            }
        }

        public Boolean PlaceBomb()
        {
            Boolean isUsed = 
                (GadgetEffect == null) && 
                GadgetAvailableSet.Contains(GadgetForLink.Bomb) && 
                (bombCount > 0);
            if (isUsed)
            {
                bombCount--;
                GadgetEffect = new BombPlaced(player);
            }

            return isUsed;
        }

        public Boolean ShootArrow()
        {
            Boolean isUsed = 
                (GadgetEffect == null) && 
                GadgetAvailableSet.Contains(GadgetForLink.Bow_Arrow) && 
                rupeeCount > 0;
            if (isUsed)
            {
                rupeeCount--;
                GadgetEffect = new ArrowShooted(player);
            }
            return isUsed;
        }

        public List<ICollider> GetGadgetCollider()
        {
            List<ICollider> collidables = new List<ICollider>();
            if (GadgetEffect != null) collidables.Add(GadgetEffect);
            if (player.state is LinkSwingSwordState)
            {
                // only create new SwordHead in sword state
                swordRectangle = new SwordHead(player.GetPos(), player.facingDirection);
                collidables.Add(swordRectangle);
            }

            return collidables;
        }
    }
}
