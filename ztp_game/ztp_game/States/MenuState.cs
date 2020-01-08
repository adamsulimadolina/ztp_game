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
    class MenuState : State
    {
        private List<Component> _components;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            
            var buttonTexture = _content.Load<Texture2D>("Components/Button");
            var buttonFont = _content.Load<SpriteFont>("Components/Font");
            var backgroundTexture = _content.Load<Texture2D>("Components/Background");

            var background = new Background(backgroundTexture)
            {
                Position = new Vector2(0, 0)
            };


            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(550, 340),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var rankingButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(550, 400),
                Text = "Ranking",
            };

            rankingButton.Click += RankingButton_Click;

            var creditsButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(550, 460),
                Text = "Credits",
            };

            creditsButton.Click += CreditsButton_Click;

            var exitButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(550, 520),
                Text = "Exit",
            };

            exitButton.Click += ExitButton_Click;

            _components = new List<Component>()
            {
                background,
                newGameButton,
                rankingButton,
                creditsButton,
                exitButton,
            };
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

        private void NewGameButton_Click(object sender, EventArgs e)
        {
           
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void RankingButton_Click(object sender, EventArgs e)
        {
            //sound.StopMusic();
            _game.ChangeState(new RankingState(_game, _graphicsDevice, _content));
        }

        private void CreditsButton_Click(object sender, EventArgs e)
        {
            //sound.StopMusic();
            _game.ChangeState(new CreditsState(_game, _graphicsDevice, _content));
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            //sound.StopMusic();
            _game.Exit();
        }
        public override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
