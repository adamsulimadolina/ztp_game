using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ztp_game.Components;
using ztp_game.Input;

namespace ztp_game.States
{
    class Button : Component
    {
        private InputManager inputManager;
        public bool faded;
        private Color colour;

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;

            _font = font;

            inputManager = InputManager.GetInstance();

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            if (Selected)
            {
                PenColour = Color.Red;
                colour = Color.Red;
            }
            else if(faded)
            {
                PenColour = Color.DarkSlateGray;
                colour = Color.DarkSlateGray;
            }
            else
            {
                colour = Color.White;
                PenColour = Color.White;
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
