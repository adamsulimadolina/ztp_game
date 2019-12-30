using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ztp_game.Sprites
{
    public abstract class Sprite
    {
        public Texture2D _texture;
        public Texture2D _texture_flip;
        public Texture2D _texture_normal;
        public Vector2 Position;
        public Vector2 Velocity;
        public Color Colour = Color.White;
        public float Speed = 4f;
        public static bool specialLevel = false;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
            set 
            {
                Rectangle = value;
            }
        }
        public Sprite()
        {
        }
        public Sprite(Texture2D texture)
        {
            _texture = texture;
            Position.X = 16;
            //Position.Y = (Screen.getHeight() - 3) * 16;
        }
        public Sprite(Texture2D texture, Texture2D texture_flip)
        {
            _texture_flip = texture_flip;
            _texture_normal = texture;
            _texture = _texture_normal;
            Position.X = 16;
            //Position.Y = (Screen.getHeight() - 3) * 16;
        }
        public Sprite(Texture2D texture, int X, int Y)
        {
            _texture = texture;
            Position.X = X;
            Position.Y = Y;
        }

        public virtual void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._texture, Position, Color.White);
        }

        #region Collision       
        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
                this.Rectangle.Left < sprite.Rectangle.Left &&
                this.Rectangle.Bottom > sprite.Rectangle.Top &&
                this.Rectangle.Top < sprite.Rectangle.Bottom;
        }
        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
                this.Rectangle.Right > sprite.Rectangle.Right &&
                this.Rectangle.Bottom > sprite.Rectangle.Top &&
                this.Rectangle.Top < sprite.Rectangle.Bottom;
        }
        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
                this.Rectangle.Top < sprite.Rectangle.Top &&
                this.Rectangle.Right > sprite.Rectangle.Left &&
                this.Rectangle.Left < sprite.Rectangle.Right;
        }
        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
                this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
                this.Rectangle.Right > sprite.Rectangle.Left &&
                this.Rectangle.Left < sprite.Rectangle.Right;
        }


        #endregion

    }

}
