using Microsoft.Xna.Framework;
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
        private Vector2 velocity;
        private Vector2 position;

        public SaveMemento(char[,] levelArray, int points, int level, int health, Vector2 velocity, Vector2 position)
        {
            this.levelArray = levelArray;
            this.points = points;
            this.level = level;
            this.health = health;
            this.velocity = velocity;
            this.position = position;
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

        public Vector2 GetVelocity()
        {
            return velocity;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

    }
}
