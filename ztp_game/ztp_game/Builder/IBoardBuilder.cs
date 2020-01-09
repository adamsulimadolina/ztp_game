using Microsoft.Xna.Framework.Content;
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
        int x { get; set; }
        int y { get; set; }
        ContentManager content { get; set; }

        void GenerateBlock();

        void GenerateThorn();

        void GenerateCoin();

        void GenerateBorder();

        void GenerateDoors(int height, int width);

        SpriteCollection GetLevel();
    }
}
