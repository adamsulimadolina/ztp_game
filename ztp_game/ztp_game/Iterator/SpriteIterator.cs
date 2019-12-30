using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Collection;
using ztp_game.Sprites;

namespace ztp_game.Iterator
{
    class SpriteIterator : ISpriteIterator
    {
        private SpriteCollection _sprites;
        private int _current = 0;
        private int _step = 1;

        public SpriteIterator(SpriteCollection sprite_list)
        {
            this._sprites = sprite_list;
        }

        public Sprite First()
        {
            _current = 0;
            return _sprites[_current] as Sprite;
        }

        public Sprite Next()
        {
            _current += _step;
            if (!IsDone)
                return _sprites[_current] as Sprite;
            else
                return null;
        }

        public Sprite CurrentSprite
        {
            get { return _sprites[_current] as Sprite; }
        }

        public bool IsDone
        {
            get { return _current >= _sprites.Count(); }
        }
    }
}
