using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using ztp_game.Input;

using ztp_game.Memento;
using ztp_game.ObserverTemplate;
using ztp_game.Sprites;
using ztp_game.States;
using ztp_game.Strategy;

namespace ztp_game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game, Observer
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputManager inputManager;

        SaveCaretaker saveCaretaker;

        SoundManager soundManager;
        private bool easyLevel;

        public void IsGameEasy(bool difficulty)
        {
            easyLevel = difficulty;
        }
        public bool getEasyLevel()
        {
            return easyLevel;
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            inputManager = InputManager.GetInstance();
            saveCaretaker = new SaveCaretaker();
            soundManager = new SoundManager(Content);
            MediaPlayer.IsRepeating = true;
        }

        private State _currentState;
        private State _nextState;
        public State currentGameState;


        public void ChangeState(State state)
        {
            if (state is GameState) currentGameState =state;
            if (state is ConfirmExitState && _currentState is GameState)
            {
                var gameState = _currentState as GameState;
                saveCaretaker.AddMemento(gameState.Save());
            }
            if (state is GameState && _currentState is ConfirmExitState)
            {
                var gameState = state as GameState;
                gameState.ReadSave(saveCaretaker.GetMemento());
            }
            if (state is GameState && _currentState is MenuState)
            {
                var gameState = state as GameState;
                gameState.ReadSave(saveCaretaker.GetMemento());
            }
            if (!(state is GameState) && _currentState is GameState && Champion.GetInstance().health > 0)
            {
                var gameState = _currentState as GameState;
                saveCaretaker.AddMemento(gameState.Save());
            }
            
            _nextState = state;
        }

        public void LoadAudioFiles()
        {
            soundManager.LoadFiles();
        }
        public void PlaySong(string name)
        {
            soundManager.PlaySong(name);
        }
        public void PlaySound(string name)
        {
            soundManager.PlaySound(name);
        }
        public void SetMusicMasterVolume(float volume)
        {
            soundManager.SetMusicMasterVolume(volume);
        }
        public void SetSoundMasterVolume(float volume)
        {
            soundManager.SetSoundMasterVolume(volume);
        }
        public float GetMusicVolume()
        {
            return soundManager.GetMusicVolume();
        }
        public float GetSoundVolume()
        {
            return soundManager.GetSoundVolume();
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //IsMouseVisible = false;
            
            graphics.PreferredBackBufferWidth = 1600;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 680;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            LoadAudioFiles();
            SetMusicMasterVolume(1.0f);
            SetSoundMasterVolume(1.0f);
            _currentState = new MenuState(this, graphics.GraphicsDevice, Content);

            ChangeState(_currentState);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }


            _currentState.Update(gameTime);
            // TODO: Add your update logic here

            inputManager.Update(gameTime);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _currentState.Draw(gameTime, spriteBatch);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void Update()
        {
            saveCaretaker.RemoveSave();
        }
    }
}
