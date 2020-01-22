using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Sprites;

namespace ztp_game.Iterator
{
    public interface ISpriteIterator
    {
        Sprite First();
        T Next<T>() where T : class;
        void RemoveCoin(Coin coin);
    }
}
