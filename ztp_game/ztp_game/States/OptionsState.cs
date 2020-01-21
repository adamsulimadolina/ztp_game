using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ztp_game.Components;
using ztp_game.Strategy;

namespace ztp_game.States
{
    class OptionsState : State
    {
        private List<Component> _components;
        private NavigationMenu navigationMenu;
        public AudioButton musicVolumeButton, soundVolumeButton;

        public OptionsState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Components/Button");
            var buttonFont = _content.Load<SpriteFont>("Components/Font");
            var backgroundTexture = _content.Load<Texture2D>("Components/Background");

            var background = new MenuBackground(backgroundTexture)
            {
                Position = new Vector2(0, 0)
            };

            musicVolumeButton = new AudioButton(buttonTexture, buttonFont, _game.GetMusicVolume())
            {
                Position = new Vector2(550, 340),
                Text = "Music volume: ",
                OnClick = ApplyMusicVolume,
            };

            soundVolumeButton = new AudioButton(buttonTexture, buttonFont, _game.GetSoundVolume())
            {
                Position = new Vector2(550, 400),
                Text = "Sounds volume: ",
                OnClick = ApplySoundVolume,
            };

            var backButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(550, 600),
                Text = "Back",
            };
            backButton.OnClick += BackButton_Click;


            navigationMenu = new NavigationMenu(new List<Component>
            {
                musicVolumeButton,
                soundVolumeButton,
                backButton,
            });
            //navigationMenu = new NavigationMenu(new List<Button>
            //{
            //    backButton,
            //});


            _components = new List<Component>()
            {
                background,
                navigationMenu,
                //navigationMenu
            };
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        private void ApplyMusicVolume(object sender, EventArgs e)
        {
            _game.SetMusicMasterVolume(musicVolumeButton.GetVolume());
        }
        private void ApplySoundVolume(object sender, EventArgs e)
        {
            _game.SetSoundMasterVolume(soundVolumeButton.GetVolume());
            _game.PlaySound("death");
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
