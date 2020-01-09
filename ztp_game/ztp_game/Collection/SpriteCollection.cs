using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Iterator;
using ztp_game.Sprites;

namespace ztp_game.Collection
{
    class SpriteCollection : ISpriteCollection
    {
        private ArrayList _items;

        public ISpriteIterator CreateIterator()
        {
            return new SpriteIterator(this);
        }

        public int Count()
        {
            return _items.Count;
        }

        public void RemoveCoin(Coin coin)
        {
            _items.Remove(coin);
        }

        public object this[int index]
        {
            get { return _items[index]; }
            set { _items.Add(value); }
        }
    }
}
