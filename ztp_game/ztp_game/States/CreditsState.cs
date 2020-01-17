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
    class CreditsState : State
    {
        private List<Component> _components;
        private SpriteFont _font;

        public CreditsState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            _font = _content.Load<SpriteFont>("Fonts/Font");
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var backgroundTexture = _content.Load<Texture2D>("Controls/background");

            var background = new Background(backgroundTexture)
            {
                Position = new Vector2(0, 0)
            };


            var backButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(550, 570),
                Text = "Go to main menu"
            };

            backButton.OnClick += BackButton_Click;

            _components = new List<Component>()
            {
                background,
                backButton
            };
            ;

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var elem in _components)
                elem.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(_font, "Adam Sulima Dolina", new Vector2(680, 340), Color.White);
            spriteBatch.DrawString(_font, "Michal Szorc", new Vector2(720, 400), Color.White);
            spriteBatch.DrawString(_font, "Piotr Awramiuk", new Vector2(710, 460), Color.White);

            spriteBatch.End();
        }

        public override void Initialize()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
