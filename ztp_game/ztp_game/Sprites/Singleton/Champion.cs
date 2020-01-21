using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Input;
using ztp_game.Logic;
using ztp_game.Collisions;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ztp_game.Collection;
using ztp_game.ObserverTemplate;
using ztp_game.Strategy;

namespace ztp_game.Sprites
{
    public class Champion : Sprite , Observable
    {
        private static Champion instance = new Champion();
        private Direction direction;
        private InputManager inputManager;
        private Collision collisions;
        private ContentManager content;
        private SoundManager soundManager;
        private SpriteCollection spriteCollection;
        private float startingSpeed = 4f;

        public int points = 0;

        public int health = 3;

        public int level;

        private List<Observer> observers = new List<Observer>();

        private Champion()
        {
            inputManager = InputManager.GetInstance();
            level = 1;

            Speed = startingSpeed + level * 2;
            SetPositionStart();
            direction = Direction.Down;
        }
        public static Champion GetInstance()
        {
            return instance;
        }

        public void SetCollection(SpriteCollection collection)
        {
            spriteCollection = collection;
        }

        public static void SetContent(ContentManager content_new)
        {
            GetInstance().content = content_new;
            GetInstance()._texture = GetInstance().content.Load<Texture2D>("Champion/Champ");
            GetInstance()._texture_flip = GetInstance().content.Load<Texture2D>("Champion/ChampFlip");
            GetInstance()._texture_normal = GetInstance()._texture;
            GetInstance().SetPositionStart();

        }
        public void SetSoundManagerContent(ContentManager content)
        {
            soundManager = new SoundManager(content);
            soundManager.LoadFiles();
        }

        public void ChangeDirection(Direction direction)
        {
            this.direction = direction;

            if (direction == Direction.Down)
                _texture = _texture_normal;
            else
                _texture = _texture_flip;
        }

        public Direction GetDirection()
        {
            return direction;

        }

        public override void Update()
        {
            Move();
            collisions = new Collision(spriteCollection);
            collisions.CollisionBlock();
            collisions.CollisionCoin();
            collisions.CollisionDoor();
            collisions.CollisionThorn();
            collisions.CollisionBorder();
            collisions.CollisionGap();
            Position += Velocity;
            Velocity = Vector2.Zero;
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

        public void ResetValues()
        {
            health = 3;
            Speed = startingSpeed + level * 2;
            SetPositionStart();
            direction = Direction.Down;
        }

        public void SetValuesToStandard()
        {
            health = 3;
            points = 0;
            level = 1;
            Speed = startingSpeed + level * 2;
            SetPositionStart();
            direction = Direction.Down;
        }

        public void LoseHealth()
        {
            
            SetPositionStart();
            this.health--;

            Speed = startingSpeed + level * 2 ;

            soundManager.PlaySound("death");
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
        }
        public void SetPositionStart()
        {
            if (!specialLevel)
            {
                Position.X = 16;
                Position.Y = (Screen.getHeight() - 3) * 16;
                this.Velocity = Vector2.Zero;
            }
            else
            {
                Position.X = (Screen.getWidth() - 3) * 16;
                Position.Y = 16;
                this.Velocity = Vector2.Zero;
            }
        }

        public void Attach(Observer observer)
        {
            observers.Add(observer);
        }

        public void Detach(Observer observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach(var obs in observers)
            {
                obs.Update();
            }
        }
    }
}
