using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class LinkSpriteFactory : ISpriteFactory
    {
		private int stepFrameCount;
		private Texture2D stepTexture;
		public Rectangle stepLeftFrameSize { get; private set; }
		public Rectangle stepRightFrameSize { get; private set; }
		public Rectangle stepFrontFrameSize { get; private set; }
		public Rectangle stepBackFrameSize { get; private set; }

		private int swingSwordFrameCount;
		private Texture2D swingSwordFrontTexture;
		private Texture2D swingSwordBackTexture;
		private Texture2D swingSwordLeftTexture;
		private Texture2D swingSwordRightTexture;
		public Rectangle swingSwordFrontFrameSize { get; private set; }
		public Rectangle swingSwordBackFrameSize { get; private set; }
		public Rectangle swingSwordLeftRightFrameSize { get; private set; }

		private int useItemFrameCount;
		private Texture2D useItemTexture;
		public Rectangle useItemFrontFrameSize { get; private set; }
		public Rectangle useItemBackFrameSize { get; private set; }
		public Rectangle useItemLeftFrameSize { get; private set; }
		public Rectangle useItemRightFrameSize { get; private set; }

		// More private Texture2Ds follow
		// ...

		private static LinkSpriteFactory instance = new LinkSpriteFactory();
		public static LinkSpriteFactory Instance
		{
			get
			{
				return instance;
			}
		}

		private LinkSpriteFactory()
		{
		}

		public void LoadAllTextures(ContentManager content)
		{
			stepFrameCount = 2;
			stepTexture = content.Load<Texture2D>("Link/LinkStep_16x16");
			stepFrontFrameSize = new Rectangle(0, 0, 16, 16);
			stepBackFrameSize = new Rectangle(32, 0, 16, 16);
			stepRightFrameSize = new Rectangle(64, 0, 16, 16);
			stepLeftFrameSize = new Rectangle(96, 0, 16, 16);

			swingSwordFrameCount = 1;
			swingSwordFrontFrameSize = new Rectangle(0, 0, 16, 27);
			swingSwordFrontTexture = content.Load<Texture2D>("Link/ZeldaSpriteLinkSwingSwordFront");
			swingSwordBackFrameSize = new Rectangle(0, 0, 16, 28);
			swingSwordBackTexture = content.Load<Texture2D>("Link/ZeldaSpriteLinkSwingSwordBack");
			swingSwordLeftRightFrameSize = new Rectangle(0, 0, 27, 15);
			swingSwordLeftTexture = content.Load<Texture2D>("Link/ZeldaSpriteLinkSwingSwordLeft");
			swingSwordRightTexture = content.Load<Texture2D>("Link/ZeldaSpriteLinkSwingSwordRight");

			useItemFrameCount = 1;
			useItemTexture = content.Load<Texture2D>("Link/LinkUseItem_16x16");
			useItemFrontFrameSize = new Rectangle(0, 0, 16, 16);
			useItemBackFrameSize = new Rectangle(16, 0, 16, 16);
			useItemRightFrameSize = new Rectangle(32, 0, 16, 16);
			useItemLeftFrameSize = new Rectangle(48, 0, 16, 16);

			// More Content.Load calls follow
			//...
		}

		public ISprite CreateLinkStepFrontSprite()
		{
			return new LinkStaticSprite(stepTexture, stepFrontFrameSize, stepFrameCount);
		}
		public ISprite CreateLinkStepBackSprite()
		{
			return new LinkStaticSprite(stepTexture, stepBackFrameSize, stepFrameCount);
		}
		public ISprite CreateLinkStepLeftSprite()
		{
			return new LinkStaticSprite(stepTexture, stepLeftFrameSize, stepFrameCount);
		}
		public ISprite CreateLinkStepRightSprite()
		{
			return new LinkStaticSprite(stepTexture, stepRightFrameSize, stepFrameCount);
		}


		public ISprite CreateLinkDamagedFrontSprite()
		{
			return new LinkDamagedSprite(stepTexture, stepFrontFrameSize);
		}
		public ISprite CreateLinkDamagedBackSprite()
		{
			return new LinkDamagedSprite(stepTexture, stepBackFrameSize);
		}
		public ISprite CreateLinkDamagedLeftSprite()
		{
			return new LinkDamagedSprite(stepTexture, stepLeftFrameSize);
		}
		public ISprite CreateLinkDamagedRightSprite()
		{
			return new LinkDamagedSprite(stepTexture, stepRightFrameSize);
		}


		public ISprite CreateLinkSwingSwordFrontSprite()
		{
			return new LinkStaticSprite(swingSwordFrontTexture, swingSwordFrontFrameSize, swingSwordFrameCount);
		}
		public ISprite CreateLinkSwingSwordBackSprite()
		{
			return new LinkStaticSprite(swingSwordBackTexture, swingSwordBackFrameSize, swingSwordFrameCount);
		}
		public ISprite CreateLinkSwingSwordLeftSprite()
		{
			return new LinkStaticSprite(swingSwordLeftTexture, swingSwordLeftRightFrameSize, swingSwordFrameCount);
		}
		public ISprite CreateLinkSwingSwordRightSprite()
		{
			return new LinkStaticSprite(swingSwordRightTexture, swingSwordLeftRightFrameSize, swingSwordFrameCount);
		}

		public ISprite CreateLinkUseItemFrontSprite()
		{
			return new LinkStaticSprite(useItemTexture, useItemFrontFrameSize, useItemFrameCount);
		}
		public ISprite CreateLinkUseItemBackSprite()
		{
			return new LinkStaticSprite(useItemTexture, useItemBackFrameSize, useItemFrameCount);
		}
		public ISprite CreateLinkUseItemLeftSprite()
		{
			return new LinkStaticSprite(useItemTexture, useItemLeftFrameSize, useItemFrameCount);
		}
		public ISprite CreateLinkUseItemRightSprite()
		{
			return new LinkStaticSprite(useItemTexture, useItemRightFrameSize, useItemFrameCount);
		}

		// More public ISprite returning methods follow
		// ...

		public ISprite CreateCloneStepFrontSprite()
		{
			return new CloneStaticSprite(stepTexture, stepFrontFrameSize, stepFrameCount);
		}
		public ISprite CreateCloneStepBackSprite()
		{
			return new CloneStaticSprite(stepTexture, stepBackFrameSize, stepFrameCount);
		}
		public ISprite CreateCloneStepLeftSprite()
		{
			return new CloneStaticSprite(stepTexture, stepLeftFrameSize, stepFrameCount);
		}
		public ISprite CreateCloneStepRightSprite()
		{
			return new CloneStaticSprite(stepTexture, stepRightFrameSize, stepFrameCount);
		}


		public ISprite CreateCloneDamagedFrontSprite()
		{
			return new LinkDamagedSprite(stepTexture, stepFrontFrameSize);
		}
		public ISprite CreateCloneDamagedBackSprite()
		{
			return new LinkDamagedSprite(stepTexture, stepBackFrameSize);
		}
		public ISprite CreateCloneDamagedLeftSprite()
		{
			return new LinkDamagedSprite(stepTexture, stepLeftFrameSize);
		}
		public ISprite CreateCloneDamagedRightSprite()
		{
			return new LinkDamagedSprite(stepTexture, stepRightFrameSize);
		}


		public ISprite CreateCloneSwingSwordFrontSprite()
		{
			return new CloneStaticSprite(swingSwordFrontTexture, swingSwordFrontFrameSize, swingSwordFrameCount);
		}
		public ISprite CreateCloneSwingSwordBackSprite()
		{
			return new CloneStaticSprite(swingSwordBackTexture, swingSwordBackFrameSize, swingSwordFrameCount);
		}
		public ISprite CreateCloneSwingSwordLeftSprite()
		{
			return new CloneStaticSprite(swingSwordLeftTexture, swingSwordLeftRightFrameSize, swingSwordFrameCount);
		}
		public ISprite CreateCloneSwingSwordRightSprite()
		{
			return new CloneStaticSprite(swingSwordRightTexture, swingSwordLeftRightFrameSize, swingSwordFrameCount);
		}

		public ISprite CreateCloneUseItemFrontSprite()
		{
			return new CloneStaticSprite(useItemTexture, useItemFrontFrameSize, useItemFrameCount);
		}
		public ISprite CreateCloneUseItemBackSprite()
		{
			return new CloneStaticSprite(useItemTexture, useItemBackFrameSize, useItemFrameCount);
		}
		public ISprite CreateCloneUseItemLeftSprite()
		{
			return new CloneStaticSprite(useItemTexture, useItemLeftFrameSize, useItemFrameCount);
		}
		public ISprite CreateCloneUseItemRightSprite()
		{
			return new CloneStaticSprite(useItemTexture, useItemRightFrameSize, useItemFrameCount);
		}
	}
}
