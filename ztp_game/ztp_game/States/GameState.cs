using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ztp_game.Logic;
using ztp_game.Sprites;

namespace ztp_game.States
{
    class GameState : State
    {
        private List<Sprite> _sprites;
        private SpriteFont _font;
        private Champion _champ;
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //_champ.Draw(spriteBatch);
            //_board.Draw(spriteBatch);
            spriteBatch.DrawString(_font, "Score: " + _champ.points + "  ", new Vector2(0, Screen.getHeight() * 16), Color.White);
            spriteBatch.DrawString(_font, "  Level: " + Screen.getLevel() + "  ", new Vector2((Screen.getWidth() / 3) * 16, Screen.getHeight() * 16), Color.White);
            spriteBatch.DrawString(_font, "  Health: " + _champ.health + "  ", new Vector2((Screen.getWidth() * 2 / 3) * 16, Screen.getHeight() * 16), Color.White);

            foreach (var sprite in _sprites)
            {
                sprite.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
                return;
            }
            if (_champ.Health <= 0)
            {
                Screen.setLevel(1);
                _game.ChangeState(new NewRecordState(_game, _graphicsDevice, _content, _champ));
                return;
            }
            _champ.Update(_sprites);
            if (Screen.getChange())
            {
                Screen.ChangeMap(false);
                _champ.points += 15;
                Screen.setLevel(Screen.getLevel() + 1);
                _champ.health = 3;

                _game.ChangeState(new GameState(_game, _graphicsDevice, _content, _champ));

            }
        }
    }
}
