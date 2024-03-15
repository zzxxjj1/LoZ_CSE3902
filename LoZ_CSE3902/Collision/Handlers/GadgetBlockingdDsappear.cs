using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace LoZ_CSE3902
{
    public class GadgetBlockingDisappear : ICommand
    {
        public Inventory inventory;

        public GadgetBlockingDisappear(ICollider gadget, ICollider anything, Direction side)
        {
            IGadgetEffect gadgetEffect = (IGadgetEffect)gadget;
            inventory = gadgetEffect.Player.inventory;
        }

        public void Execute()
        {
            inventory.TerminateGadget();
        }
    }
}
