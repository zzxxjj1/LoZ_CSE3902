using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LoZ_CSE3902
{
    public class NPCSpriteFactory : ISpriteFactory
    {
        private static Texture2D bossTexture;
        private static Texture2D bossDamagedTexture;
        private static Texture2D oldManTexture;
        private static Texture2D fireballTexture;
        private static Texture2D gelTexture;
        private static Texture2D kesseTexture;
        private static Texture2D stalfosTexture;
        private static Texture2D stalfosDamagedTexture;
        private static Texture2D trapTexture;
        private static Texture2D wallMasterURTexture;
        private static Texture2D wallMasterULTexture;
        private static Texture2D wallMasterDRTexture;
        private static Texture2D wallMasterDLTexture;
        private static Texture2D goriyaFrontTexture;
        private static Texture2D goriyaFrontDamagedTexture;
        private static Texture2D goriyaBackTexture;
        private static Texture2D goriyaBackDamagedTexture;
        private static Texture2D goriyaLeftTexture;
        private static Texture2D goriyaLeftDamagedTexture;
        private static Texture2D goriyaRightTexture;
        private static Texture2D goriyaRightDamagedTexture;
        private static Texture2D goriyaBoomerangTexture;
        private static Texture2D explosionTexture;

        private static readonly Rectangle BOSS = new Rectangle(0, 0, 24, 32);
        private static readonly Rectangle OLD_MAN_IDLE = new Rectangle(0, 0, 16, 16);
        private static readonly Rectangle FIREBALL = new Rectangle(0, 0, 9, 10);
        private static readonly Rectangle GEL = new Rectangle(0, 0, 8, 9);
        private static readonly Rectangle KESSE = new Rectangle(0, 0, 16, 10);
        private static readonly Rectangle STALFOS = new Rectangle(0, 0, 16, 16);
        private static readonly Rectangle TRAP_IDLE = new Rectangle(0, 0, 16, 16);
        private static readonly Rectangle WALLMASTER = new Rectangle(0, 0, 16, 16);
        private static readonly Rectangle GORIYA_FONT = new Rectangle(0, 0, 14, 16);
        private static readonly Rectangle GORIYA_SIDE = new Rectangle(0, 0, 15, 16);
        private static readonly Rectangle EXPLOSION = new Rectangle(0, 0, 18, 16);
        private static readonly Rectangle GORIYA_BOOMERANG = new Rectangle(0, 0, 8, 8);


        private static int TWO_FRAME = 2;
        private static int FOUR_FRAME = 4;

        private static NPCSpriteFactory instance = new NPCSpriteFactory();

        public static NPCSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public NPCSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            bossTexture = content.Load<Texture2D>("NPC/DragonLeft");
            bossDamagedTexture = content.Load<Texture2D>("NPC/DragonDamaged");
            oldManTexture = content.Load<Texture2D>("NPC/OldMan");
            fireballTexture = content.Load<Texture2D>("NPC/Fireball");
            gelTexture = content.Load<Texture2D>("NPC/Gel");
            kesseTexture = content.Load<Texture2D>("NPC/Kesse");
            stalfosTexture = content.Load<Texture2D>("NPC/Stalfos");
            stalfosDamagedTexture = content.Load<Texture2D>("NPC/StalfosDamaged");
            trapTexture = content.Load<Texture2D>("NPC/Trap");
            wallMasterURTexture = content.Load<Texture2D>("NPC/WallMasterUR");
            wallMasterULTexture = content.Load<Texture2D>("NPC/WallMasterUL");
            wallMasterDRTexture = content.Load<Texture2D>("NPC/WallMasterDR");
            wallMasterDLTexture = content.Load<Texture2D>("NPC/WallMasterDL");
            goriyaFrontTexture = content.Load<Texture2D>("NPC/GoriyaBlueFront");
            goriyaFrontDamagedTexture = content.Load<Texture2D>("NPC/GoriyaFrontDamaged");
            goriyaBackTexture = content.Load<Texture2D>("NPC/GoriyaBlueBack");
            goriyaBackDamagedTexture = content.Load<Texture2D>("NPC/GoriyaBackDamaged");
            goriyaLeftTexture = content.Load<Texture2D>("NPC/GoriyaBlueLeft");
            goriyaLeftDamagedTexture = content.Load<Texture2D>("NPC/GoriyaLeftDamaged");
            goriyaRightTexture = content.Load<Texture2D>("NPC/GoriyaBlueRight");
            goriyaRightDamagedTexture = content.Load<Texture2D>("NPC/GoriyaRightDamaged");
            goriyaBoomerangTexture = content.Load<Texture2D>("NPC/GoriyaBoomerang");
            explosionTexture = content.Load<Texture2D>("NPC/Explosion");
        }

        public ISprite CreateBossSprite()
        {
            return new NPCAnimatedSprite(bossTexture, BOSS, TWO_FRAME);
        }

        public ISprite CreateBossDamagedSprite()
        {
            return new NPCAnimatedSprite(bossDamagedTexture, BOSS, FOUR_FRAME);
        }

        public ISprite CreateOldManSprite()
        {
            return new NPCStaticSprite(oldManTexture, OLD_MAN_IDLE);
        }

        public ISprite CreateFireballSprite()
        {
            return new NPCAnimatedSprite(fireballTexture, FIREBALL, FOUR_FRAME);
        }

        public ISprite CreateGelSprite()
        {
            return new NPCAnimatedSprite(gelTexture, GEL, TWO_FRAME);
        }

        public ISprite CreateKesseSprite()
        {
            return new NPCAnimatedSprite(kesseTexture, KESSE, TWO_FRAME);
        }

        public ISprite CreateStalfosSprite()
        {
            return new NPCAnimatedSprite(stalfosTexture, STALFOS, TWO_FRAME);
        }

        public ISprite CreateStalfosDamagedSprite()
        {
            return new NPCAnimatedSprite(stalfosDamagedTexture, STALFOS, FOUR_FRAME);
        }

        public ISprite CreateTrapSprite()
        {
            return new NPCStaticSprite(trapTexture, TRAP_IDLE);
        }

        public ISprite CreateWallMasterURSprite()
        {
            return new NPCAnimatedSprite(wallMasterURTexture, WALLMASTER, TWO_FRAME);
        }

        public ISprite CreateWallMasterULSprite()
        {
            return new NPCAnimatedSprite(wallMasterULTexture, WALLMASTER, TWO_FRAME);
        }

        public ISprite CreateWallMasterDRSprite()
        {
            return new NPCAnimatedSprite(wallMasterDRTexture, WALLMASTER, TWO_FRAME);
        }

        public ISprite CreateWallMasterDLSprite()
        {
            return new NPCAnimatedSprite(wallMasterDLTexture, WALLMASTER, TWO_FRAME);
        }

        public ISprite CreateGoriyaFrontSprite()
        {
            return new NPCAnimatedSprite(goriyaFrontTexture, GORIYA_FONT, TWO_FRAME);
        }

        public ISprite CreateGoriyaFrontDamagedSprite()
        {
            return new NPCAnimatedSprite(goriyaFrontDamagedTexture, GORIYA_FONT, FOUR_FRAME);
        }

        public ISprite CreateGoriyaBackSprite()
        {
            return new NPCAnimatedSprite(goriyaBackTexture, GORIYA_FONT, TWO_FRAME);
        }

        public ISprite CreateGoriyaBackDamagedSprite()
        {
            return new NPCAnimatedSprite(goriyaBackDamagedTexture, GORIYA_FONT, FOUR_FRAME);
        }

        public ISprite CreateGoriyaLeftSprite()
        {
            return new NPCAnimatedSprite(goriyaLeftTexture, GORIYA_SIDE, TWO_FRAME);
        }

        public ISprite CreateGoriyaLeftDamagedSprite()
        {
            return new NPCAnimatedSprite(goriyaLeftDamagedTexture, GORIYA_SIDE, FOUR_FRAME);
        }

        public ISprite CreateGoriyaRightSprite()
        {
            return new NPCAnimatedSprite(goriyaRightTexture, GORIYA_SIDE, TWO_FRAME);
        }

        public ISprite CreateGoriyaRightDamagedSprite()
        {
            return new NPCAnimatedSprite(goriyaRightDamagedTexture, GORIYA_SIDE, FOUR_FRAME);
        }

        public ISprite CreateGoriyaBoomerangSprite()
        {
            return new NPCAnimatedSprite(goriyaBoomerangTexture, GORIYA_BOOMERANG, FOUR_FRAME);
        }

        public ISprite CreateExplosionSprite()
        {
            return new NPCAnimatedSprite(explosionTexture, EXPLOSION, FOUR_FRAME);
        }
    }
}
