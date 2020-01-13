using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Memento;
using ztp_game.Sprites;

namespace ztp_game.States
{
    public abstract class State
    {
        #region Fields

        protected ContentManager _content;

        protected GraphicsDevice _graphicsDevice;

        protected Game1 _game;

        protected SaveCaretaker saveCaretaker;

        #endregion

        #region Methods

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Initialize();

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;
            saveCaretaker = game.GetSaveCaretaker();
        }

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Champion champ)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;
            saveCaretaker = game.GetSaveCaretaker();
        }

        public abstract void Update(GameTime gameTime);

        #endregion
    }
}
