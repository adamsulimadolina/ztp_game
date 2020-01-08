using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Input;

namespace ztp_game.Sprites
{
    public class Champion: Sprite
    {
        private static Champion instance = new Champion();
        private Direction direction;
        private InputManager inputManager;
        //private List<Observer> observersList;
        public int points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
            }
        }
        public int health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        public int level = 0;
        //{
        //    get
        //    {
        //        //return level;
        //    }
        //    set
        //    {
        //        //level = value;
        //    }
        //}

        private Champion()
        {
            inputManager = new InputManager();
            level = 0;
            //dodanie tekstury
        }
        public static Champion GetInstance()
        {
            return instance;
        }

        public void ChangeDirection(Direction direction)
        {
            this.direction = direction;
        }

        public override void Update()
        {
            Move();
            //Sprawdzanie kolizji
        }

        private void Move()
        {
            if (inputManager.ActionIsPressed("MoveDown"))
            {
                if (direction != Direction.Down)
                {
                    _texture = _texture_normal;
                }
                direction = Direction.Down;

            }

            if (inputManager.ActionIsPressed("MoveUp"))
            {
                if (direction != Direction.Up)
                {
                    _texture = _texture_flip;
                }
                direction = Direction.Up;
            }

            if (inputManager.ActionIsPressed("MoveLeft"))
            {
                Velocity.X = -Speed;
            }

            if (inputManager.ActionIsPressed("MoveRight"))
            {
                Velocity.X = Speed;
            }

            if (direction == Direction.Up)
            {
                Velocity.Y = -Speed;

            }
            if (direction == Direction.Down)
            {
                Velocity.Y = Speed;
            }
        }

        private void ResetValues()
        {
            health = 3;
            points = 0;
            level = 1;
            SetPositionStart();
            direction = Direction.Down;
        }

        private void GetPoint(Sprite coin, List<Sprite> sprites)
        {
            sprites.Remove(coin);
            Velocity = Vector2.Zero;
            this.points++;
        }

        private void LoseHealth()
        {
            SoundPlayer sound = new SoundPlayer("death.wav");
            SetPositionStart();
            this.health--;
            if (!specialLevel)
            {
                direction = Direction.Down;
                this._texture = _texture_normal;
            }
            else
            {
                direction = Direction.Up;
                this._texture = _texture_flip;
            }
            
            //sound.PlayMusic();
        }
        private void SetPositionStart()
        {
            if (!specialLevel)
            {
                Position.X = 16;
                //Position.Y = (Screen.getHeight() - 3) * 16;
                this.Velocity = Vector2.Zero;
            }
            else
            {
                //Position.X = (Screen.getWidth() - 3) * 16;
                Position.Y = 16;
                this.Velocity = Vector2.Zero;
            }
        }

    }
}
