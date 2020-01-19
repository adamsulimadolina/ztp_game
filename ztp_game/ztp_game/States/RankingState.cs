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
        private SpriteFont _font;
        public RankingState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Components/Button");
            _font = _content.Load<SpriteFont>("Components/Font");           
            var backgroundTexture = _content.Load<Texture2D>("Components/Background");

            var background = new Background(backgroundTexture)
            {
                Position = new Vector2(0, 0)
            };

            var backButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(550, 570),
                Text = "Go to main menu"
            };
            backButton.OnClick += backButton_Click;

            _components = new List<Component>()
            {
                backButton
            };

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Wczytywanie pliku (Memento)

            /*var placements = RankingFile.getPlacements();
            Vector2 vector = new Vector2(780, 55);

            */
            spriteBatch.Begin();

            spriteBatch.DrawString(_font, "Tu będą rankingi", new Vector2(680, 340), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
            /*
            foreach (var placement in placements)
            {
                spriteBatch.DrawString(_font, placement.name + " " + placement.score, vector, Color.White);
                vector.Y += 50;
            }
            */
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
