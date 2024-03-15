using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoZ_CSE3902
{
	// define player actions
    public class LinkClone : ICollider
    {
		public Game1 game;
		public LinkPlayer player1;
        public ILinkState state;

		// attributes not changed in game (may be changed by extra mechanism)
		public int LinkWidth = GameAttributes.Player.Width;
		public int LinkHeight = GameAttributes.Player.Height;
		public float WalkSpeed = GameAttributes.Player.WalkSpeed * 1.3f;
		public int FramesPerStep = GameAttributes.Player.FramesPerStep - 2;
		public int FramesToSwingSword = GameAttributes.Player.FramesToSwingSword - 5;
		public int FramesToUseItem = GameAttributes.Player.FramesToUseItem;
		public int DistanceSlideByDamage = GameAttributes.Player.SlideByDamage / 2;
		public int HitboxScale = -4;

		// attributes usually changed in game
		public int maxHealth = GameAttributes.Player.StartMaxHealth; // 6 half-heart == 3 full-heart
		public int health = GameAttributes.Player.StartMaxHealth;
		public Vector2 pos;
		public Vector2 previousPos;
		//public float xPos, yPos;
		public Direction facingDirection;
		public GadgetForLink itemInUse;
		public SwordHead swordHitbox;

		public LinkClone(Game1 game, LinkPlayer player)
		{
			this.game = game;
			player1 = player;

			state = new CloneStepState(this);
			facingDirection = player.facingDirection;
			itemInUse = GadgetForLink.Null;

			pos = player.GetPos();
		}

		public void Update()
		{
			if (game.gameState is GamePlayState)
				state.Update();
			previousPos = pos;
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			state.Draw(spriteBatch);

			if (HitBox.showBoarder)
				GameUtility.Instance.DrawBoarder(GetRectangle(), Color.LightYellow, 1);
			if (HitBox.showBoarder && state is CloneSwingSwordState)
			{
				swordHitbox.Draw(spriteBatch);
			}
		}

		public void MoveUp()
		{
			if (!GameAttributes.Paused) state.MoveUp();
		}

		public void MoveDown()
		{
			if (!GameAttributes.Paused) state.MoveDown();
		}

		public void MoveLeft()
		{
			if (!GameAttributes.Paused) state.MoveLeft();
		}
		public void MoveRight()
		{
			if (!GameAttributes.Paused) state.MoveRight();
		}
		public void UseItemA()
		{
			if (!GameAttributes.Paused) state.UseItemA();
		}

		public void PickUp(ItemInGame item)
		{
			player1.PickUp(item);
		}
		public void TakeDamage()
		{
			if (!GameAttributes.Paused) state.TakeDamage();
		}

		public void SetPos(Vector2 pos)
        {
			this.pos.X = pos.X;
			this.pos.Y = pos.Y;
        }

		public Vector2 GetPos()
		{
			return pos;
		}
		public Vector2 GetPreviousPos()
        {
			return previousPos;
        }

		public void ChangeFacing(Direction direction)
        {
			facingDirection = direction;
			state = new CloneStepState(this);
		}

		public Rectangle GetRectangle()
		{
			return HitBox.Create(pos.ToPoint(), HitBox.Link.Body, new Point(HitboxScale));
		}

		public List<ICollider> GetColliderList()
		{
			List<ICollider> collidables = new List<ICollider>();
			collidables.Add(this);
			if (state is CloneSwingSwordState)
			{
				// only create new SwordHead in sword state
				swordHitbox = new SwordHead(pos, facingDirection);
				collidables.Add(swordHitbox);
			}

			return collidables;
		}

		public void Healing(int amount)
        {
			player1.Healing(amount);
        }

		public void IncreaseHeartLimit(int amount)
		{
			//player1.IncreaseHeartLimit(amount);
			// intended for player 1 to pick up heart container
		}
	}
}
