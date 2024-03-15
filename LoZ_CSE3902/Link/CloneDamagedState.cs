using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LoZ_CSE3902
{
    public class CloneDamagedState : ILinkState
    {
        private LinkClone player;
        private ISprite sprite;
        private float distance;
        private const float SlideSpeed = 2; // per frame

        private Direction side; // keep facing direction in this state.

        public CloneDamagedState(LinkClone player)
        {
            this.player = player;
            player.player1.health--;
            distance = player.DistanceSlideByDamage;
            side = player.facingDirection;
            SoundManager.Instance.Play(SoundEnum.Link_Hurt);

            switch (player.facingDirection)
            {
                case (Direction.Down):
                    sprite = LinkSpriteFactory.Instance.CreateLinkDamagedFrontSprite();
                    break;
                case (Direction.Up):
                    sprite = LinkSpriteFactory.Instance.CreateLinkDamagedBackSprite();
                    break;
                case (Direction.Left):
                    sprite = LinkSpriteFactory.Instance.CreateLinkDamagedLeftSprite();
                    break;
                case (Direction.Right):
                    sprite = LinkSpriteFactory.Instance.CreateLinkDamagedRightSprite();
                    break;
                default: break;
            }
        }
        public void Update()
        {
            sprite.Update();

            distance -= SlideSpeed;
            bool isStop = distance <= 0;

            var pos = player.GetPos();
            switch (side)
            {
                case (Direction.Down): pos.Y -= SlideSpeed; break;
                case (Direction.Up): pos.Y += SlideSpeed; break;
                case (Direction.Left): pos.X += SlideSpeed; break;
                case (Direction.Right): pos.X -= SlideSpeed; break;
                default: break;
            }
            ApplyPosition(pos);
            if (isStop)
            {
                player.state = new CloneStepState(player);
                player.state.SetInvincibleFrames(60);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, player.GetPos());
        }

        public void ApplyPosition(Vector2 pos)
        {
            Room currentRoom = ((GamePlayState)player.game.gameState).room;

            // prevent link is repelled into the room.
            if (currentRoom.data.InUnderworld)
            {
                if (pos.X < GameAttributes.Room.UnderworldBoundary.X)
                    pos.X = GameAttributes.Room.UnderworldBoundary.X;
                if (pos.X > GameAttributes.Room.UnderworldBoundary.Width)
                    pos.X = GameAttributes.Room.UnderworldBoundary.Width;
                if (pos.Y < GameAttributes.Room.UnderworldBoundary.Y)
                    pos.Y = GameAttributes.Room.UnderworldBoundary.Y;
                if (pos.Y > GameAttributes.Room.UnderworldBoundary.Height)
                    pos.Y = GameAttributes.Room.UnderworldBoundary.Height;
            } else
            {
                if (pos.X < GameAttributes.Room.OverworldBoundary.X)
                    pos.X = GameAttributes.Room.OverworldBoundary.X;
                if (pos.X > GameAttributes.Room.OverworldBoundary.Width)
                    pos.X = GameAttributes.Room.OverworldBoundary.Width;
                if (pos.Y < GameAttributes.Room.OverworldBoundary.Y)
                    pos.Y = GameAttributes.Room.OverworldBoundary.Y;
                if (pos.Y > GameAttributes.Room.OverworldBoundary.Height)
                    pos.Y = GameAttributes.Room.OverworldBoundary.Height;
            }

            player.SetPos(pos);            
        }

        // cannot do anything until recovered
        public void MoveUp()
        {
        }
        public void MoveDown()
        {
        }
        public void MoveLeft()
        {
        }
        public void MoveRight()
        {
        }
        public void UseItemA()
        {
        }
        public void UseItemB()
        {

        }
        public void PickUp()
        {

        }
        public void TakeDamage()
        {
        }
        public void SetInvincibleFrames(int frames)
        {

        }
    }
}

