using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Collection;
using ztp_game.Sprites;

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
        private Direction direction;
        private int pick;
        private float speed;

        public SaveMemento(char[,] levelArray, int points, int level, int health, Vector2 velocity, Vector2 position, Direction direction, int pick, float speed)
        {
            this.levelArray = levelArray;
            this.points = points;
            this.level = level;
            this.health = health;
            this.velocity = velocity;
            this.position = position;
            this.direction = direction;
            this.pick = pick;
            this.speed = speed;
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

        public Direction GetDirection()
        {
            return direction;
        }

        public int GetPick()
        {
            return pick;
        }

        public float GetSpeed()
        {
            return speed;
        }

    }
}
