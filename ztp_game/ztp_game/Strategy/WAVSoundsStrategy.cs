using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ztp_game.Strategy
{
    class WAVSoundsStrategy : ISoundStrategy
    {
        List<SoundEffect> soundsList;
        public string name;
        private SoundEffect sound;

        public WAVSoundsStrategy(List<SoundEffect> soundFilesList,string soundName)
        {
            soundsList = soundFilesList;
            name = soundName;
        }
        public WAVSoundsStrategy(List<SoundEffect> soundFilesList)
        {
            soundsList = soundFilesList;
        }


            public void PlayFile()
        {
            sound = soundsList.Find(x => x.Name == name);
            sound.CreateInstance().Play();
        }

        public void SetMasterVolume(float volume)
        {
            SoundEffect.MasterVolume = volume;
        }
        public float GetMasterVolume()
        {
            return SoundEffect.MasterVolume;
        }
    }
}
