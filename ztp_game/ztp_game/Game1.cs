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
        protected override void Initialize()
        {
            
            graphics.PreferredBackBufferWidth = 1600;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 680;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            LoadAudioFiles();
            SetMusicMasterVolume(0.5f);
            SetSoundMasterVolume(1.0f);
            _currentState = new MenuState(this, graphics.GraphicsDevice, Content);

            ChangeState(_currentState);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {

            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }
            _currentState.Update(gameTime);           

            inputManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _currentState.Draw(gameTime, spriteBatch);
            
            base.Draw(gameTime);
        }

        public void Update()
        {
            saveCaretaker.RemoveSave();
        }
    }
}
