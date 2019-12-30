using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Iterator;
using ztp_game.Sprites;

namespace ztp_game.Collection
{
    interface ISpriteCollection
    {
        ISpriteIterator CreateIterator();
        int Count();
    }
}
