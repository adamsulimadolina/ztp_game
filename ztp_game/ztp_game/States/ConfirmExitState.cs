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
    class ConfirmExitState : State
    {
        private List<Component> _components;
        private NavigationMenu navigationMenu;
        private SpriteFont _font;
        public ConfirmExitState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Components/Button");
            var buttonFont = _content.Load<SpriteFont>("Components/Font");
            _font = buttonFont;

            var continueButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(550, 400),
                Text = "Continue",
            };
            continueButton.OnClick += ContinueButton_Click;

            var exitButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(550, 460),
                Text = "Quit",
            };
            exitButton.OnClick += ExitButton_Click;

            navigationMenu = new NavigationMenu(new List<Component>
            {
                continueButton,
                exitButton,
            });
            _components = new List<Component>()
            {                
                navigationMenu,
            };
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, false));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(_font, "Are you shure you want to quit game?",new Vector2(550,200),Color.White);

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
