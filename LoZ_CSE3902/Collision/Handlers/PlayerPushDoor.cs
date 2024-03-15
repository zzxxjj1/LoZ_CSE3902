using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class PlayerPushDoor : ICommand
    {
        private Game1 game;
        private LinkPlayer player;
        private Door door;

        private ICommand possibleCommand;

        public PlayerPushDoor(ICollider player, ICollider door, Direction side)
        {
            this.player = (LinkPlayer)player;
            game = this.player.game;
            this.door = (Door)door;

            if (this.door.state.Equals(DoorState.Open) ||
                this.door.state.Equals(DoorState.Destructed))
                possibleCommand = new GoToNextRoom(game);
            else possibleCommand = new PlayerBlockingMovement(player, door, side);
        }


        public void Execute()
        {
            door.Unlock(player);
            possibleCommand.Execute();
        }
    }
}
