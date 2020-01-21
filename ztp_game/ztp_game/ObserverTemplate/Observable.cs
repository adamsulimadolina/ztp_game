using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ztp_game.ObserverTemplate
{
    public interface Observable
    {
        void Attach(Observer observer);
        void Detach(Observer observer);
        void NotifyObservers();
    }
}
