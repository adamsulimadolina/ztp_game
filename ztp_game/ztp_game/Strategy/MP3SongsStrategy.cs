using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ztp_game.Strategy
{
    class MP3SongsStrategy : ISoundStrategy
    {
        public List<Song> songsList;
        public Song currentSong;
        public Song song;
        public string name;
        public MP3SongsStrategy(List<Song> songFilesList,string songName,Song currentSongPlaying)
        {
            songsList = songFilesList;
            currentSong = currentSongPlaying;
            name = songName;
        }
        public MP3SongsStrategy(List<Song> songFilesList)
        {
            songsList = songFilesList;
        }

        public bool IsThisMusicFilePlaying()
        {
            if (currentSong == null) return false;
            else if (currentSong.Name == song.Name) return true;
            return false;
        }

       
        public void PlayFile()
        {
            song = songsList.Find(x => x.Name == name);
            if (IsThisMusicFilePlaying()) return;
            if (song == null)
            {
                currentSong = null;
                MediaPlayer.Stop();
                return;
            }
            MediaPlayer.Stop();
            MediaPlayer.Play(song);
        }
        public void SetMasterVolume(float volume)
        {
            MediaPlayer.Volume = volume;
        }
        public float GetMasterVolume()
        {
            return MediaPlayer.Volume;
        }
    }
}
