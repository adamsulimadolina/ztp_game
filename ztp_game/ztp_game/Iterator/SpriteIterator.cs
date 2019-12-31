using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Collection;

namespace ztp_game.Iterator
{
    class SpriteIterator : ISpriteIterator
    {
        private ISpriteCollection _sprites;
        private int _current = 0;
        private int _step = 1;
        private int _sprite_to_iterate;


        public SpriteIterator(SpriteCollection sprite_list)
        {
            this._sprites = sprite_list;
        }
    }
}
