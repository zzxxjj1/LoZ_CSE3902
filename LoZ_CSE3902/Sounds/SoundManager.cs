using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace LoZ_CSE3902
{
    public class SoundManager
    {
        private SoundEffect[] soundContents;
        private List<SoundEffectInstance> instanceList;
        //private Dictionary<SoundEnum, SoundEffectInstance> instances;
        private SoundEnum currentBGM;
        private HashSet<SoundEnum> pausedByCommand;

        private static SoundManager instance = new SoundManager();

        public static SoundManager Instance
        {
            get
            {
                return instance;
            }
        }

        public SoundManager()
        {
        }

        public void LoadAllResources(ContentManager content)
        {
            pausedByCommand = new HashSet<SoundEnum>();

            soundContents = new SoundEffect[Enum.GetNames(typeof(SoundEnum)).Length];
            soundContents[(int)SoundEnum.Arrow_Boomerang] = content.Load<SoundEffect>("Sounds/LOZ_Arrow_Boomerang");
            soundContents[(int)SoundEnum.Bomb_Drop] = content.Load<SoundEffect>("Sounds/LOZ_Bomb_Drop");
            soundContents[(int)SoundEnum.Bomb_Blow] = content.Load<SoundEffect>("Sounds/LOZ_Bomb_Blow");
            soundContents[(int)SoundEnum.Boss_Scream1] = content.Load<SoundEffect>("Sounds/LOZ_Boss_Scream1");
            soundContents[(int)SoundEnum.Door_Unlock] = content.Load<SoundEffect>("Sounds/LOZ_Door_Unlock");
            soundContents[(int)SoundEnum.Enemy_Die] = content.Load<SoundEffect>("Sounds/LOZ_Enemy_Die");
            soundContents[(int)SoundEnum.Enemy_Hit] = content.Load<SoundEffect>("Sounds/LOZ_Enemy_Hit");
            soundContents[(int)SoundEnum.Get_Heart] = content.Load<SoundEffect>("Sounds/LOZ_Get_Heart");
            soundContents[(int)SoundEnum.Get_Item] = content.Load<SoundEffect>("Sounds/LOZ_Get_Item");
            soundContents[(int)SoundEnum.Get_Rupee] = content.Load<SoundEffect>("Sounds/LOZ_Get_Rupee");
            soundContents[(int)SoundEnum.Key_Appear] = content.Load<SoundEffect>("Sounds/LOZ_Key_Appear");
            soundContents[(int)SoundEnum.Link_Hurt] = content.Load<SoundEffect>("Sounds/LOZ_Link_Hurt");
            soundContents[(int)SoundEnum.Sword_Slash] = content.Load<SoundEffect>("Sounds/LOZ_Sword_Slash");
            soundContents[(int)SoundEnum.BGM_Overworld] = content.Load<SoundEffect>("Sounds/BGM_Overworld");
            soundContents[(int)SoundEnum.BGM_Underworld] = content.Load<SoundEffect>("Sounds/BGM_Underworld");
            soundContents[(int)SoundEnum.BGM_Title] = content.Load<SoundEffect>("Sounds/BGM_Title");
            soundContents[(int)SoundEnum.BGM_GameOver] = content.Load<SoundEffect>("Sounds/BGM_Game_Over");
            soundContents[(int)SoundEnum.Catch_Triforce] = content.Load<SoundEffect>("Sounds/Catch_Triforce");

            instanceList = new List<SoundEffectInstance>(soundContents.Length);
            for (int i = 0; i < soundContents.Length; i++)
            {
                instanceList.Add(soundContents[i].CreateInstance());
            }

            currentBGM = SoundEnum.BGM_Title;
            instanceList[(int)currentBGM].IsLooped = true;
        }

        public void Play(SoundEnum label)
        {
            instanceList[(int)label].Stop();
            instanceList[(int)label].Play();
        }
        public void Pause(SoundEnum label)
        {
            instanceList[(int)label].Pause();
        }
        public void Resume(SoundEnum label)
        {
            instanceList[(int)label].Resume();
        }
        public void Stop(SoundEnum label)
        {
            instanceList[(int)label].Stop();
        }

        public void StopAll()
        {
            foreach (SoundEffectInstance instance in instanceList)
            {
                instance.Stop();
            }
        }

        public void SetBGM(SoundEnum label)
        {
            instanceList[(int)currentBGM].Stop();
            currentBGM = label;
            instanceList[(int)currentBGM].Play();
            instanceList[(int)currentBGM].IsLooped = true;
        }

        public void PlayBGM()
        {
            instanceList[(int)currentBGM].Play();
        }
        public void PauseBGM()
        {
            instanceList[(int)currentBGM].Play();
        }
        public void StopBGM()
        {
            instanceList[(int)currentBGM].Stop();
        }

        public void GamePaused()
        {
            if (GameAttributes.Paused)
            {
                for (int i = 0; i < instanceList.Count; i++)
                {
                    if (instanceList[i].State == SoundState.Playing)
                    {
                        instanceList[i].Pause();
                        pausedByCommand.Add((SoundEnum)i);
                    }
                }
            } else
            {
                foreach(SoundEnum label in pausedByCommand)
                {
                    instanceList[(int)label].Play();
                }
                pausedByCommand.Clear();
            }
        }


    }
}
