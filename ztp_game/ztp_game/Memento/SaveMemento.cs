using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Collection;

namespace ztp_game.Memento
{
    public class SaveMemento
    {
        private char[,] levelArray;
        private int points;
        private int level;
        private int health;

        public SaveMemento(char[,] levelArray, int points, int level, int health)
        {
            this.levelArray = levelArray;
            this.points = points;
            this.level = level;
            this.health = health;
        }

        public char[,] GetLevelArray()
        {
            return levelArray;
        }

        public int GetPoints()
        {
            return points;
        }

        public int GetLevel()
        {
            return level;
        }

        public int GetHealth()
        {
            return health;
        }

    }
}
