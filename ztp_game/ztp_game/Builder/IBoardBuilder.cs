using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Collection;

namespace ztp_game.Builder
{
    interface IBoardBuilder
    {
        SpriteCollection sprite_collection { get; set; }

        void GenerateBlock();

        void GenerateThorn();

        SpriteCollection GetLevel();
    }
}
