using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class ItemSpriteFactory : ISpriteFactory
    {
        private const int StaticSpriteFrameCount = 1;

        private Texture2D ArrowTexture;
        public Rectangle ArrowFrameSize;

        private static Texture2D BombTexture;
        public Rectangle BombFrameSize;

        private static Texture2D BoomerangTexture;
        public Rectangle BoomerangFrameSize;

        private static Texture2D BowTexture;
        public Rectangle BowFrameSize;

        private static Texture2D ClockTexture;
        public Rectangle ClockFrameSize;

        private static Texture2D CompassTexture;
        public Rectangle CompassFrameSize;

        private static Texture2D FairyTexture;
        public Rectangle FairyFrameSize { get; private set; }

        private static Texture2D HeartTexture;
        public Rectangle HeartFrameSize { get; private set; }
        private int HeartFrameCount;

        private static Texture2D HeartContainerTexture;
        public Rectangle HeartContainerFrameSize;

        private static Texture2D KeyTexture;
        public Rectangle KeyFrameSize;

        private static Texture2D MapTexture;
        public Rectangle MapFrameSize;

        private static Texture2D RupeeTexture;
        public Rectangle RupeeFrameSize { get; private set; }
        private int RupeeFrameCount;

        private static Texture2D SwordTexture;
        public Rectangle SwordFrameSize;

        private static Texture2D TriforceTexture;
        public Rectangle TriforceFrameSize { get; private set; }
        private int TriforceFrameCount;

        private static Texture2D WhiteSwordTexture;
        public Rectangle WhiteSwordFrameSize { get; private set; }

        private static Texture2D SwordDownTexture;
        public Rectangle SwordDownFrameSize{ get; private set; }

        private static Texture2D SwordLeftTexture;
        public Rectangle SwordLeftFrameSize { get; private set; }

        private static Texture2D SwordRightTexture;
        public Rectangle SwordRightFrameSize { get; private set; }

        private static Texture2D SwordUpTexture;
        public Rectangle SwordUpFrameSize { get; private set; }

        private static Texture2D BombSmokeTexture;
        public Rectangle BombSmokeFrameSize;
        public int BombSmokeFrameCount;

        private Texture2D ArrowShootedTexture;
        public Rectangle ArrowShootedUpFrameSize { get; private set; }
        public Rectangle ArrowShootedDownFrameSize { get; private set; }
        public Rectangle ArrowShootedLeftFrameSize { get; private set; }
        public Rectangle ArrowShootedRightFrameSize { get; private set; }

        public ItemSpriteFactory()
        {
        }

        private static ItemSpriteFactory instance = new ItemSpriteFactory();

        public static ItemSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }


        public void LoadAllTextures(ContentManager content)
        {
            ArrowTexture = content.Load<Texture2D>("Items/ZeldaSpriteArrow");
            ArrowFrameSize = new Rectangle(0, 0, 8, 16);

            BombTexture = content.Load<Texture2D>("Items/ZeldaSpriteBomb");
            BombFrameSize = new Rectangle(0, 0, 8, 14);

            BoomerangTexture = content.Load<Texture2D>("Items/ZeldaSpriteBoomerang");
            BoomerangFrameSize = new Rectangle(0, 0, 5, 9);

            BowTexture = content.Load<Texture2D>("Items/ZeldaSpriteBow");
            BowFrameSize = new Rectangle(0, 0, 8, 16);

            ClockTexture = content.Load<Texture2D>("Items/ZeldaSpriteClock");
            ClockFrameSize = new Rectangle(0, 0, 11, 16);

            CompassTexture = content.Load<Texture2D>("Items/ZeldaSpriteCompass");
            CompassFrameSize = new Rectangle(0, 0, 11, 12);

            FairyTexture = content.Load<Texture2D>("Items/ZeldaSpriteFairy_8x16");
            FairyFrameSize = new Rectangle(0, 0, 8, 16);

            HeartTexture = content.Load<Texture2D>("Items/ZeldaSpriteHeart_7x8");
            HeartFrameSize = new Rectangle(0, 0, 7, 8);
            HeartFrameCount = 2;

            HeartContainerTexture = content.Load<Texture2D>("Items/ZeldaSpriteHeartContainer");
            HeartContainerFrameSize = new Rectangle(0, 0, 13, 12);

            KeyTexture = content.Load<Texture2D>("Items/ZeldaSpriteKey");
            KeyFrameSize = new Rectangle(0, 0, 8, 16);

            MapTexture = content.Load<Texture2D>("Items/ZeldaSpriteMap");
            MapFrameSize = new Rectangle(0, 0, 8, 16);

            RupeeTexture = content.Load<Texture2D>("Items/ZeldaSpriteRupy_8x16");
            RupeeFrameSize = new Rectangle(8, 0, 8, 16);
            RupeeFrameCount = 2;

            SwordTexture = content.Load<Texture2D>("Items/ZeldaSpriteSword");
            SwordFrameSize = new Rectangle(0, 0, 7, 16);

            TriforceTexture = content.Load<Texture2D>("Items/ZeldaSpriteTriforce_10x10");
            TriforceFrameSize = new Rectangle(0, 0, 10, 10);
            TriforceFrameCount = 2;

            WhiteSwordTexture = content.Load<Texture2D>("Items/ZeldaSpriteWhiteSword");
            WhiteSwordFrameSize = new Rectangle(0, 0, 7, 16);

            SwordDownTexture = content.Load<Texture2D>("Items/sworddown");
            SwordDownFrameSize = new Rectangle(0, 0, 7, 16);

            SwordLeftTexture = content.Load<Texture2D>("Items/swordleft");
            SwordLeftFrameSize = new Rectangle(0, 0, 16, 7);

            SwordRightTexture = content.Load<Texture2D>("Items/swordright");
            SwordRightFrameSize = new Rectangle(0, 0, 16, 7);

            SwordUpTexture = content.Load<Texture2D>("Items/swordup");
            SwordUpFrameSize = new Rectangle(0, 0, 7, 16);

            BombSmokeTexture = content.Load<Texture2D>("Items/BombSmoke");
            BombSmokeFrameSize = new Rectangle(0, 0, 44, 46);
            BombSmokeFrameCount = 4;

            ArrowShootedTexture = content.Load<Texture2D>("Items/Arrow_16x16");
            ArrowShootedUpFrameSize = new Rectangle(0, 0, 16, 16);
            ArrowShootedDownFrameSize = new Rectangle(16, 0, 16, 16);
            ArrowShootedLeftFrameSize = new Rectangle(48, 0, 16, 16);
            ArrowShootedRightFrameSize = new Rectangle(32, 0, 16, 16);
        }


        public ISprite CreateArrowSprite()
        {
            return new ItemSprite(ArrowTexture, ArrowFrameSize, StaticSpriteFrameCount);
        }

        public ISprite CreateBombSprite()
        {
            return new ItemSprite(BombTexture, BombFrameSize, StaticSpriteFrameCount);
        }

        public ISprite CreateBoomerangSprite()
        {
            return new ItemSprite(BoomerangTexture, BoomerangFrameSize, StaticSpriteFrameCount);
        }

        public ISprite CreateBowSprite()
        {
            return new ItemSprite(BowTexture, BowFrameSize, StaticSpriteFrameCount);
        }

        public ISprite CreateClockSprite()
        {
            return new ItemSprite(ClockTexture, ClockFrameSize, StaticSpriteFrameCount);
        }

        public ISprite CreateCompassSprite()
        {
            return new ItemSprite(CompassTexture, CompassFrameSize, StaticSpriteFrameCount);
        }

        public ISprite CreateFairySprite()
        {
            return new ItemSprite(FairyTexture, FairyFrameSize, StaticSpriteFrameCount);
        }

        public ISprite CreateHeartSprite()
        {
            return new ItemSprite(HeartTexture, HeartFrameSize, HeartFrameCount);
        }

        public ISprite CreateHeartContainerSprite()
        {
            return new ItemSprite(HeartContainerTexture, HeartContainerFrameSize, StaticSpriteFrameCount);
        }

        public ISprite CreateKeySprite()
        {
            return new ItemSprite(KeyTexture, KeyFrameSize, StaticSpriteFrameCount);
        }

        public ISprite CreateMapSprite()
        {
            return new ItemSprite(MapTexture, MapFrameSize, StaticSpriteFrameCount);
        }

        public ISprite CreateRupeeSprite()
        {
            return new ItemSprite(RupeeTexture, RupeeFrameSize, RupeeFrameCount);
        }

        public ISprite CreateSwordSprite()
        {
            return new ItemSprite(SwordTexture, SwordFrameSize, StaticSpriteFrameCount);
        }

        public ISprite CreateTriforceSprite()
        {
            return new ItemSprite(TriforceTexture, TriforceFrameSize, TriforceFrameCount);
        }

        public ISprite CreateArrowShootedUpSprite()
        {
            return new ItemSprite(ArrowShootedTexture, ArrowShootedUpFrameSize, StaticSpriteFrameCount);
        }
        public ISprite CreateArrowShootedDownSprite()
        {
            return new ItemSprite(ArrowShootedTexture, ArrowShootedDownFrameSize, StaticSpriteFrameCount);
        }
        public ISprite CreateArrowShootedLeftSprite()
        {
            return new ItemSprite(ArrowShootedTexture, ArrowShootedLeftFrameSize, StaticSpriteFrameCount);
        }
        public ISprite CreateArrowShootedRightSprite()
        {
            return new ItemSprite(ArrowShootedTexture, ArrowShootedRightFrameSize, StaticSpriteFrameCount);
        }

        public ISprite CreateWhiteSwordSprite()
        {
            return new ItemSprite(SwordUpTexture, SwordUpFrameSize, StaticSpriteFrameCount);
        }

        public ISprite CreateSwordShootedUpSprite()
        {
            return new ItemSprite(SwordUpTexture, SwordUpFrameSize, StaticSpriteFrameCount);
        }
        public ISprite CreateSwordShootedDownSprite()
        {
            return new ItemSprite(SwordDownTexture, SwordDownFrameSize, StaticSpriteFrameCount);
        }
        public ISprite CreateSwordShootedLeftSprite()
        {
            return new ItemSprite(SwordLeftTexture, SwordLeftFrameSize, StaticSpriteFrameCount);
        }
        public ISprite CreateSwordShootedRightSprite()
        {
            return new ItemSprite(SwordRightTexture, SwordRightFrameSize, StaticSpriteFrameCount);
        }
        public ISprite CreateBombSmokeSprite()
        {
            return new ItemSprite(BombSmokeTexture, BombSmokeFrameSize, BombSmokeFrameCount);
        }

    }
}
