using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Components;

namespace ztp_game.States
{
    class RankingState : State
    {
        private List<Component> _components;
        private NavigationMenu navigationMenu;
        private SpriteFont _font;
        public RankingState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Components/Button");
            var buttonFont = _content.Load<SpriteFont>("Components/Font");
            _font = buttonFont;

            var backButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(550, 570),
                Text = "Go to main menu"
            };
            backButton.OnClick += backButton_Click;

            navigationMenu = new NavigationMenu(new List<Component>
            {
                backButton,
            });
            _components = new List<Component>()
            {
                navigationMenu,
            };

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var placements = RankingFile.getPlacements();
            Vector2 vector = new Vector2(780, 55);

            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            foreach (var placement in placements)
            {
                spriteBatch.DrawString(_font, placement.name + " " + placement.score, vector, Color.White);
                vector.Y += 50;
            }

            spriteBatch.End();
        }

      

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

    }
}
