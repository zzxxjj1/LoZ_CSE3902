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
    public class LinkPlayer : ICollider
    {
		public Game1 game;
        public ILinkState state;

		// attributes not changed in game (may be changed by extra mechanism)
		public int LinkWidth = GameAttributes.Player.Width;
		public int LinkHeight = GameAttributes.Player.Height;
		public float WalkSpeed = GameAttributes.Player.WalkSpeed;
		public int FramesPerStep = GameAttributes.Player.FramesPerStep;
		public int FramesToSwingSword = GameAttributes.Player.FramesToSwingSword;
		public int FramesToUseItem = GameAttributes.Player.FramesToUseItem;
		public int DistanceSlideByDamage = GameAttributes.Player.SlideByDamage;

		// attributes usually changed in game
		public int maxHealth = GameAttributes.Player.StartMaxHealth; // 6 half-heart == 3 full-heart
		public int health = GameAttributes.Player.StartMaxHealth;
		public Vector2 pos;
		public Vector2 previousPos;
		//public float xPos, yPos;
		public Direction facingDirection;
		public GadgetForLink itemInUse;
		public Inventory inventory;

		public LinkPlayer(Game1 game)
		{
			this.game = game;
			Initialize();
		}

		private void Initialize()
        {
			state = new LinkStepState(this);
			facingDirection = Direction.Up;
			itemInUse = GadgetForLink.Null;
			inventory = new Inventory(this);

			// start position
			Vector2 startPos = GameAttributes.Player.Pos.StartPos;
			SetPos(Room.ConvertGridToScreenPosition(startPos, true));
		}

		public void Update()
		{
			inventory.Update();
			state.Update();
			previousPos = pos;
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			inventory.Draw(spriteBatch);
			state.Draw(spriteBatch);

			if (HitBox.showBoarder)
				GameUtility.Instance.DrawBoarder(GetRectangle(), Color.Gold, 1);
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
		public void UseItemB()
		{
			if (!GameAttributes.Paused)
				if (inventory.UseGadget())
					state.UseItemB();
		}
		public void PickUp(ItemInGame item)
		{
			state.PickUp();
			//inventory.PickUp(item);
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
			state = new LinkStepState(this);
		}

		public Rectangle GetRectangle()
		{
			return HitBox.Create(pos.ToPoint(), HitBox.Link.Body, new Point(-1));
		}

		public List<ICollider> GetColliderList()
		{
			List<ICollider> collidables = new List<ICollider>();
			collidables.Add(this);
			collidables.AddRange(inventory.GetGadgetCollider());

			if (game.gameState is GamePlayState)
			{
				GamePlayState gameplay = (GamePlayState)game.gameState;
				if (gameplay.isDuoPlay)
					collidables.AddRange(gameplay.clone.GetColliderList());
			}

			return collidables;
		}

		public bool UseKey()
		{
			return inventory.UseKey();
		}

		public void Healing(int amount)
        {
			health += amount;
			if (health > maxHealth) health = maxHealth;
        }
		public void IncreaseHeartLimit(int amount)
		{
			maxHealth += amount;
			if (maxHealth > GameAttributes.Player.HealthLimit)
				maxHealth = GameAttributes.Player.HealthLimit;
			//Healing(amount);
		}

		public void SetGadget(GadgetForLink gadget)
        {
			if (inventory.GadgetAvailableSet.Contains(gadget))
				itemInUse = gadget;
        }
	}
}
