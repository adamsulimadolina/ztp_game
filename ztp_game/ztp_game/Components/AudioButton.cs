using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ztp_game.Input;

namespace ztp_game.Components
{
    class AudioButton : Component
    {
        private float volume;
        private InputManager inputManager;

        public AudioButton(Texture2D texture, SpriteFont font,float vol)
        {
            _texture = texture;
            _font = font;
            volume = vol;
            inputManager = InputManager.GetInstance();
        }

        public void SetVolume(float vol)
        {
            volume = vol;
        }
        public float GetVolume()
        {
            return volume;
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

                spriteBatch.DrawString(_font, Text + volume * 100, new Vector2(x, y), PenColour);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Selected)
            {
                if (inputManager.ActionWasJustPressed("MoveLeft"))
                {
                    if (volume > 0.0f)
                    {
                        volume -= 0.1f;
                        volume = (float)Math.Round(volume, 1);
                    }
                    OnClick?.Invoke(this, new EventArgs());

                }
                else if (inputManager.ActionWasJustPressed("MoveRight"))
                {
                    if (volume < 1.0f)
                    {
                        volume += 0.1f;
                        volume = (float)Math.Round(volume, 1);
                    }
                    OnClick?.Invoke(this, new EventArgs());
                }
                if (volume < 0.1f) volume = 0.0f;
                if (volume > 1.0f) volume = 1.0f;
                if(inputManager.ActionWasJustPressed("Accept"))
                {
                    
                }
            }
        }
    }
}
