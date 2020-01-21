using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ztp_game.Components;

namespace ztp_game.States
{
    class DifficultyState : State
    {
        private List<Component> _components;
        private NavigationMenu navigationMenu;

        public DifficultyState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Components/Button");
            var buttonFont = _content.Load<SpriteFont>("Components/Font");
            var backgroundTexture = _content.Load<Texture2D>("Components/Background");
            var background = new MenuBackground(backgroundTexture)
            {
                Position = new Vector2(0, 0)
            };

            var easyGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(550, 280),
                Text = "Easy",
            };
            easyGameButton.OnClick += EasyGameButton_Click;

            var hardGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(550, 340),
                Text = "Hard",
            };
            hardGameButton.OnClick += HardGameButton_Click;

            navigationMenu = new NavigationMenu(new List<Component>
            {
                easyGameButton,
                hardGameButton,
            });
            _components = new List<Component>()
            {
                background,
                navigationMenu,
            };
        }

        private void EasyGameButton_Click(object sender, EventArgs e)
        {
            _game.IsGameEasy(true);
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, true));
        }
        private void HardGameButton_Click(object sender, EventArgs e)
        {
            _game.IsGameEasy(false);
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, true));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
