using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ztp_game.Components
{
    public abstract class Component
    {
        protected Texture2D _texture;
        protected SpriteFont _font;
        public Color PenColour { get; set; }
        public string Text { get; set; }
        public Vector2 Position { get; set; }


        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }


        public EventHandler OnClick { get; set; }
        public EventHandler OnSelectedChange { get; set; }


        private bool selected;
        public bool Selected
        {
            get => selected;
            set
            {
                if (selected == value)
                    return;
                else
                {
                    selected = value;
                    OnSelectedChange?.Invoke(this, new EventArgs());
                }
            }
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
        

    }
}
