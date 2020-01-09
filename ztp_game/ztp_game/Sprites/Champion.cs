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

namespace ztp_game.Sprites
{
    public class Champion : Sprite
    {
        private static Champion instance = new Champion();
        private Direction direction;
        private InputManager inputManager;
        private Collision collisions;
        private ContentManager content;
        private SpriteCollection spriteCollection;

        //private List<Observer> observersList;
        public int points = 0;

        public int health = 3;

        public int level = 0;

        private Champion()
        {
            inputManager = Game1.inputManager;
            level = 0;

            Speed = 4f;
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

        public void ChangeDirection(Direction direction)
        {
            this.direction = direction;
        }

        public override void Update()
        {
            Move();
            collisions = new Collision(spriteCollection);
            collisions.CollisionBlock();
            collisions.CollisionCoin();
            collisions.CollisionDoor();
            collisions.CollisionGap();
            collisions.CollisionThorn();
            collisions.CollisionBorder();
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

        public void LoseHealth()
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

    }
}
