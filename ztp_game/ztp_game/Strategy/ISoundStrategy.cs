using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ztp_game.Strategy
{
    public interface ISoundStrategy
    {
        void PlayFile();
        void SetMasterVolume(float volume);
        float GetMasterVolume();
    }
}
