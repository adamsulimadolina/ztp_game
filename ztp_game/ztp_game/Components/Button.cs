using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Components;
using ztp_game.Input;

namespace ztp_game.States
{
    class Button : Component
    {
        private SpriteFont _font;

        private bool selected;

        private InputManager inputManager;

        public EventHandler OnClick { get; set; }
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
        public EventHandler OnSelectedChange { get; set; }

        public Color PenColour { get; set; }

        public string Text { get; set; }

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;

            _font = font;

            inputManager = InputManager.GetInstance();

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            PenColour = Color.White;

            if (Selected)
            {
                PenColour = Color.Red;
                colour = Color.Red;
            }

            spriteBatch.Draw(_texture, Rectangle, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Selected)
            {
                if (inputManager.ActionWasJustPressed("Accept"))
                {
                    OnClick?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
